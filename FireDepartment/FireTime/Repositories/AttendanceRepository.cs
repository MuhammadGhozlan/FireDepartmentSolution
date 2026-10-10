using AutoMapper;
using Azure.Core;
using FireTime.Dtos.Attendance;
using FireTime.Dtos.Lookups;
using FireTime.Interfaces.Repo_Interfaces;
using FireTime.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FireTime.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly IMapper _mapper;
        private IisFireTimeContext _context;
        public AttendanceRepository(IMapper _mapper, IisFireTimeContext _context)
        {
            this._context = _context;
            this._mapper = _mapper;
        }

        public async Task<List<AttendanceResponse>> GetAllAttendance() => _mapper.Map<List<AttendanceResponse>>(await _context.Attendances.Where(att => att.DeletedInd == false).ToListAsync());

        public async Task<List<AttendanceStatusResponse>> GetAttendanceStatuses()
        {
            return await _context.AttendanceStatuses.AsNoTracking()
                .OrderBy(status => status.AttendanceStatusDesc)
                .Select(status => new AttendanceStatusResponse(status.AttendanceStatusId, status.AttendanceStatusCde, status.AttendanceStatusDesc))
                .ToListAsync();
        }

        public async Task<List<AttendanceAssignmentResponse>> GetAttendanceAssignments(string roic, DateOnly date)
        {
            return await _context.EmployeeAssignments.AsNoTracking()
                .Where(ea => !ea.DeletedInd && ea.Roic == roic
                    && ea.AssignmentStartDate <= date
                    && (ea.AssignmentEndDate == null || ea.AssignmentEndDate >= date))
                .OrderBy(ea => ea.CompanyPosition.Company.CompanyNme)
                .ThenBy(ea => ea.CompanyPosition.Shift.ShiftCode)
                .Select(ea => new AttendanceAssignmentResponse(
                    ea.EmployeeAssignmentId, ea.Roic, ea.AssignmentStartDate, ea.AssignmentEndDate,
                    ea.IsTemp, ea.CompanyPositionId, ea.CompanyPosition.Company.CompanyNme,
                    ea.CompanyPosition.Shift.ShiftCode, ea.WorkPeriod.WorkPeriodNbr))
                .ToListAsync();
        }

        public async Task<List<AttendanceRosterResponse>> GetAttendanceRoster(DateOnly date, int workPeriodNbr)
        {
            var assignments = await _context.EmployeeAssignments.AsNoTracking()
                .Where(ea => !ea.DeletedInd
                    && ea.AssignmentStartDate <= date
                    && (ea.AssignmentEndDate == null || ea.AssignmentEndDate >= date)
                    && ea.WorkPeriod.WorkPeriodNbr == workPeriodNbr)
                .OrderBy(ea => ea.CompanyPosition.Company.CompanyNme)
                .ThenBy(ea => ea.RoicNavigation.LastNme)
                .ThenBy(ea => ea.RoicNavigation.FirstNme)
                .Select(ea => new
                {
                    ea.EmployeeAssignmentId,
                    ea.Roic,
                    ea.RoicNavigation.EmployeeNbr,
                    ea.RoicNavigation.FirstNme,
                    ea.RoicNavigation.LastNme,
                    ea.CompanyPosition.Company.CompanyNme,
                    ea.CompanyPosition.Shift.ShiftCode,
                    ea.WorkPeriod.WorkPeriodNbr
                })
                .ToListAsync();

            if(assignments.Count == 0)
                return new List<AttendanceRosterResponse>();

            var assignmentIds = assignments.Select(ea => ea.EmployeeAssignmentId).ToList();
            var attendance = await _context.Attendances.AsNoTracking()
                .Where(att => !att.DeletedInd && att.AttendanceDate == date
                    && assignmentIds.Contains(att.EmployeeAssignmentId))
                .OrderBy(att => att.AttendanceId)
                .ToListAsync();
            var attendanceByAssignment = attendance.ToLookup(att => att.EmployeeAssignmentId);

            return assignments.Select(ea => new AttendanceRosterResponse(
                date, ea.EmployeeAssignmentId, ea.Roic, ea.EmployeeNbr, ea.FirstNme,
                ea.LastNme, ea.CompanyNme, ea.ShiftCode, ea.WorkPeriodNbr,
                _mapper.Map<List<AttendanceResponse>>(attendanceByAssignment[ea.EmployeeAssignmentId].ToList())))
                .ToList();
        }

        public async Task<List<AttendanceResponse>> TakeAttendance(List<AttendanceRequest> attendanceRequestList)
        {
            if(attendanceRequestList == null || attendanceRequestList.Count == 0)
                throw new ArgumentException("At least one attendance record is required.");

            var attendanceList = _mapper.Map<List<Attendance>>(attendanceRequestList);
            DateTime now = DateTime.Now;
            var batchKeys = new HashSet<(int AssignmentId, DateOnly Date)>();
            await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

            for(int i = 0; i < attendanceList.Count; i++)
            {
                var attendance = attendanceList[i];
                var request = attendanceRequestList[i];

                if(request.EmployeeAssignmentId.HasValue)
                {
                    var specificAssignment = await _context.EmployeeAssignments
                        .Where(ea => !ea.DeletedInd
                                  && ea.EmployeeAssignmentId == request.EmployeeAssignmentId.Value
                                  && ea.Roic == attendance.Roic)
                        .FirstOrDefaultAsync(ea => ea.AssignmentStartDate <= attendance.AttendanceDate
                                                && (ea.AssignmentEndDate == null || ea.AssignmentEndDate >= attendance.AttendanceDate));

                    attendance.EmployeeAssignmentId = specificAssignment?.EmployeeAssignmentId
                        ?? throw new KeyNotFoundException($"Assignment ID {request.EmployeeAssignmentId.Value} is invalid, deleted, or does not cover {attendance.AttendanceDate} for ROIC {attendance.Roic}.");
                }
                else
                {
                    var matchingAssignments = await _context.EmployeeAssignments
                        .Where(ea => !ea.DeletedInd
                                  && ea.Roic == attendance.Roic
                                  && ea.AssignmentStartDate <= attendance.AttendanceDate
                                  && (ea.AssignmentEndDate == null || ea.AssignmentEndDate >= attendance.AttendanceDate))
                        .ToListAsync();

                    if(matchingAssignments.Count == 0)
                    {
                        throw new KeyNotFoundException($"Active assignment not found for ROIC {attendance.Roic} on {attendance.AttendanceDate}.");
                    }

                    if(matchingAssignments.Count > 1)
                    {
                        throw new ArgumentException($"Multiple overlapping assignments found for ROIC {attendance.Roic} on {attendance.AttendanceDate}. Specify EmployeeAssignmentId.");
                    }

                    attendance.EmployeeAssignmentId = matchingAssignments[0].EmployeeAssignmentId;
                }

                var key = (attendance.EmployeeAssignmentId, attendance.AttendanceDate);
                if(!batchKeys.Add(key) || await _context.Attendances.AnyAsync(att => !att.DeletedInd
                    && att.EmployeeAssignmentId == key.EmployeeAssignmentId && att.AttendanceDate == key.AttendanceDate))
                {
                    throw new ArgumentException($"Attendance already exists for assignment {key.EmployeeAssignmentId} on {key.AttendanceDate}.");
                }

                attendance.CreatedDate = now;
                attendance.LastUpdateDate = now;
                attendance.CreatedBy = "System";
                attendance.LastUpdateBy = "System";
                attendance.DeletedInd = false;
            }

            await _context.Attendances.AddRangeAsync(attendanceList);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return _mapper.Map<List<AttendanceResponse>>(attendanceList);
        }

        public async Task<AttendanceResponse?> UpdateAttendance(UpdateAttendanceRequest updateAttendanceRequest, int id)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
            var attendance = await _context.Attendances
                .Where(att => !att.DeletedInd)
                .FirstOrDefaultAsync(att => att.AttendanceId == id);

            if(attendance == null)
                return null;

            var originalAssignmentId = attendance.EmployeeAssignmentId;
            var originalDate = attendance.AttendanceDate;

            var targetRoic = !string.IsNullOrWhiteSpace(updateAttendanceRequest.Roic)
                ? updateAttendanceRequest.Roic
                : attendance.Roic;

            var targetDate = updateAttendanceRequest.AttendanceDate ?? attendance.AttendanceDate;

            // Assignment validation
            if(updateAttendanceRequest.EmployeeAssignmentId.HasValue)
            {
                var specificAssignment = await _context.EmployeeAssignments
                    .Where(ea => !ea.DeletedInd
                              && ea.EmployeeAssignmentId == updateAttendanceRequest.EmployeeAssignmentId.Value
                              && ea.Roic == targetRoic)
                    .FirstOrDefaultAsync(ea => ea.AssignmentStartDate <= targetDate
                                            && (ea.AssignmentEndDate == null || ea.AssignmentEndDate >= targetDate));

                attendance.EmployeeAssignmentId = specificAssignment?.EmployeeAssignmentId
                    ?? throw new KeyNotFoundException($"Assignment ID {updateAttendanceRequest.EmployeeAssignmentId.Value} is invalid, deleted, or does not cover {targetDate} for ROIC {targetRoic}.");

                attendance.Roic = targetRoic;
                attendance.AttendanceDate = targetDate;
            }
            else if(targetRoic != attendance.Roic || updateAttendanceRequest.AttendanceDate.HasValue)
            {
                var matchingAssignments = await _context.EmployeeAssignments
                    .Where(ea => !ea.DeletedInd
                              && ea.Roic == targetRoic
                              && ea.AssignmentStartDate <= targetDate
                              && (ea.AssignmentEndDate == null || ea.AssignmentEndDate >= targetDate))
                    .ToListAsync();

                if(matchingAssignments.Count == 0)
                    throw new KeyNotFoundException($"Active assignment not found for ROIC {targetRoic} on {targetDate}.");

                if(matchingAssignments.Count > 1)
                    throw new ArgumentException($"Multiple overlapping assignments found for ROIC {targetRoic} on {targetDate}. Specify EmployeeAssignmentId.");

                attendance.EmployeeAssignmentId = matchingAssignments[0].EmployeeAssignmentId;
                attendance.Roic = targetRoic;
                attendance.AttendanceDate = targetDate;
            }

            if((attendance.EmployeeAssignmentId != originalAssignmentId || attendance.AttendanceDate != originalDate)
                && await _context.Attendances.AnyAsync(att => !att.DeletedInd
                    && att.AttendanceId != id
                    && att.EmployeeAssignmentId == attendance.EmployeeAssignmentId
                    && att.AttendanceDate == attendance.AttendanceDate))
            {
                throw new ArgumentException($"Attendance already exists for assignment {attendance.EmployeeAssignmentId} on {attendance.AttendanceDate}.");
            }

            // 1. Status update
            if(updateAttendanceRequest.AttendanceStatusId.HasValue)
                attendance.AttendanceStatusId = updateAttendanceRequest.AttendanceStatusId.Value;

            // 2. Clear or update comment (single check)
            if(updateAttendanceRequest.AttendanceComments != null)
            {
                attendance.AttendanceComments = string.IsNullOrWhiteSpace(updateAttendanceRequest.AttendanceComments)
                    ? null
                    : updateAttendanceRequest.AttendanceComments;
            }

            attendance.LastUpdateDate = DateTime.Now;
            attendance.LastUpdateBy = "System";

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return _mapper.Map<AttendanceResponse>(attendance);
        }
        public async Task<AttendanceResponse?> DeleteAttendance(int id)
        {
            var attendance = await _context.Attendances.Where(att => att.DeletedInd == false).FirstOrDefaultAsync(att => att.AttendanceId == id);
            if(attendance == null)
            {
                return null;
            }
            attendance.DeletedInd = true;
            attendance.LastUpdateDate = DateTime.Now;
            attendance.LastUpdateBy = "System";
            await _context.SaveChangesAsync();
            return _mapper.Map<AttendanceResponse>(attendance);

        }
        public async Task<List<AttendanceResponse>> FilterAttendance(AttendanceFilterRequest attendanceFilterRequest)
        {

            var query = _context.Attendances.Where(att => !att.DeletedInd);

            if(attendanceFilterRequest != null)
            {

                if(attendanceFilterRequest.AttendanceDate.HasValue && attendanceFilterRequest.AttendanceDate != default)
                {
                    query = query.Where(att => att.AttendanceDate == attendanceFilterRequest.AttendanceDate);
                }
                if(attendanceFilterRequest.StartDate.HasValue)
                {
                    query = query.Where(att => att.AttendanceDate >= attendanceFilterRequest.StartDate.Value);
                }

                if(attendanceFilterRequest.EndDate.HasValue)
                {
                    query = query.Where(att => att.AttendanceDate <= attendanceFilterRequest.EndDate.Value);
                }

                if(!string.IsNullOrWhiteSpace(attendanceFilterRequest.Roic))
                {
                    query = query.Where(att => att.Roic == attendanceFilterRequest.Roic);
                }

                if(attendanceFilterRequest.EmployeeAssignmentId != null && attendanceFilterRequest.EmployeeAssignmentId != 0)
                {
                    query = query.Where(att => att.EmployeeAssignmentId == attendanceFilterRequest.EmployeeAssignmentId);
                }

                if(attendanceFilterRequest.AttendanceStatusId != null && attendanceFilterRequest.AttendanceStatusId != 0)
                {
                    query = query.Where(att => att.AttendanceStatusId == attendanceFilterRequest.AttendanceStatusId);
                }

                if(!string.IsNullOrWhiteSpace(attendanceFilterRequest.AttendanceComments))
                {
                    query = query.Where(att => att.AttendanceComments!.ToLower() == attendanceFilterRequest.AttendanceComments.ToLower());
                }
            }
            var attendances = await query.ToListAsync();
            return _mapper.Map<List<AttendanceResponse>>(attendances);
        }

    }
}
