using AutoMapper;
using Azure.Core;
using FireTime.Dtos.Attendance;
using FireTime.Dtos.Lookups;
using FireTime.Interfaces.Repo_Interfaces;
using FireTime.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<AttendanceResponse>?> TakeAttendance(List<AttendanceRequest> attendanceRequestList)
        {           
            var attendanceList = new List<Attendance>();
            foreach(var request in attendanceRequestList)
            {
                //check if the user supplied an assignment or not
                var existingAssignment = request.EmployeeAssignmentId.HasValue;
                int assignmentId = 0;
                if(existingAssignment)
                {
                    // if the user supplied an assignment
                    var assignment = await _context.EmployeeAssignments.Where(ea => !ea.DeletedInd
                                                            && ea.AssignmentStartDate <= request.AttendanceDate
                                                            && (ea.AssignmentEndDate == null || ea.AssignmentEndDate >= request.AttendanceDate)
                                                            && ea.EmployeeAssignmentId == request.EmployeeAssignmentId
                                                            && ea.Roic == request.Roic).FirstOrDefaultAsync();
                    if(assignment == null)
                    {
                        throw new KeyNotFoundException($"No active assignment found for ROIC {request.Roic} on {request.AttendanceDate}.");
                    }
                    assignmentId = request.EmployeeAssignmentId!.Value;
                }
                else
                {
                    // if the user did not supply an assignment, find the active assignment for the ROIC on the attendance date
                    var activeAssignments = await _context.EmployeeAssignments.Where(ea => !ea.DeletedInd
                                                            && ea.AssignmentStartDate <= request.AttendanceDate
                                                            && (ea.AssignmentEndDate == null || ea.AssignmentEndDate >= request.AttendanceDate)
                                                            && ea.Roic == request.Roic).ToListAsync();
                    if(!activeAssignments.Any())
                    {
                        throw new KeyNotFoundException($"No active assignments found for ROIC {request.Roic} on {request.AttendanceDate}.");
                    }
                    if(activeAssignments.Count > 1)
                    {
                        throw new InvalidOperationException($"Multiple active assignments found for ROIC {request.Roic} on {request.AttendanceDate}.");
                    }
                    assignmentId = activeAssignments.First().EmployeeAssignmentId;
                }
                if(attendanceList.Any(a => a.EmployeeAssignmentId == assignmentId && a.AttendanceDate == request.AttendanceDate))
                {
                    throw new ArgumentException($"Duplicate entry in request for assignment ID {assignmentId} on {request.AttendanceDate}.");
                }

                var attendanceExists = await _context.Attendances.AnyAsync(att => att.Roic == request.Roic
                                                                           && att.AttendanceDate == request.AttendanceDate
                                                                           && !att.DeletedInd
                                                                           && att.EmployeeAssignmentId == assignmentId);
                if(attendanceExists)
                    continue; // Skip adding this attendance if it already exists
                else
                {
                    var attendance = new Attendance
                    {
                        AttendanceDate = request.AttendanceDate,
                        Roic = request.Roic,
                        EmployeeAssignmentId = assignmentId,
                        AttendanceStatusId = request.AttendanceStatusId,
                        AttendanceComments = request.AttendanceComments,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        LastUpdateDate = DateTime.Now,
                        LastUpdateBy = "System",
                        DeletedInd = false
                    };
                    attendanceList.Add(attendance);
                }
            }
            if(attendanceList.Any())
            {
                await _context.Attendances.AddRangeAsync(attendanceList);
                await _context.SaveChangesAsync();
            }
            var response = _mapper.Map<List<AttendanceResponse>>(attendanceList);
            return response;
        }

        public async Task<AttendanceResponse?> UpdateAttendance(UpdateAttendanceRequest updateAttendanceRequest, int id)
        {
            var attendance = await _context.Attendances.Where(att => att.DeletedInd == false && att.AttendanceId == id).FirstOrDefaultAsync();
            if(attendance == null)
            {
                throw new KeyNotFoundException($"Attendance record not found for ID {id}.");
            }
            if(updateAttendanceRequest.AttendanceDate.HasValue && updateAttendanceRequest.AttendanceDate.Value != default)
            {
                var targetDate = updateAttendanceRequest.AttendanceDate.Value;

                var isValidAssignment = await _context.EmployeeAssignments.AnyAsync(
                    ea => !ea.DeletedInd && ea.AssignmentStartDate <= targetDate &&
                          (ea.AssignmentEndDate == null || ea.AssignmentEndDate >= targetDate) &&
                          ea.EmployeeAssignmentId == attendance.EmployeeAssignmentId
                );

                if(!isValidAssignment)
                {
                    var activeAssignments = await _context.EmployeeAssignments
                        .Where(ea => ea.Roic == attendance.Roic &&
                                     ea.AssignmentStartDate <= targetDate &&
                                     (ea.AssignmentEndDate == null || ea.AssignmentEndDate >= targetDate) &&
                                     !ea.DeletedInd)
                        .Select(ea => ea.EmployeeAssignmentId)
                        .ToListAsync();

                    if(!activeAssignments.Any())
                    {
                        throw new KeyNotFoundException($"No active assignments found for ROIC {attendance.Roic} on {targetDate}.");
                    }

                    if(activeAssignments.Count > 1)
                    {
                        throw new InvalidOperationException($"Multiple active assignments found for ROIC {attendance.Roic} on {targetDate}.");
                    }

                    attendance.EmployeeAssignmentId = activeAssignments.First();
                }
                var isDuplicate = await _context.Attendances.AnyAsync(att =>
                                  !att.DeletedInd
                                  && att.AttendanceId != id
                                  && att.EmployeeAssignmentId == attendance.EmployeeAssignmentId
                                  && att.AttendanceDate == targetDate);

                if(isDuplicate)
                {
                    throw new InvalidOperationException($"An attendance record already exists for ROIC {attendance.Roic} on {targetDate}.");
                }
                attendance.AttendanceDate = targetDate;
            }
            if(updateAttendanceRequest.AttendanceStatusId.HasValue && updateAttendanceRequest.AttendanceStatusId.Value != 0)
            {
                attendance.AttendanceStatusId = updateAttendanceRequest.AttendanceStatusId.Value;
            }
            if(updateAttendanceRequest.AttendanceComments != null)
            {
                attendance.AttendanceComments = updateAttendanceRequest.AttendanceComments;
            }
            attendance.LastUpdateDate = DateTime.Now;
            attendance.LastUpdateBy = "System";
            await _context.SaveChangesAsync();
            return _mapper.Map<AttendanceResponse>(attendance);

        }
        public async Task<AttendanceResponse?> DeleteAttendance(int id)
        {
            var attendance = await _context.Attendances.Where(att => att.DeletedInd == false).FirstOrDefaultAsync(att => att.AttendanceId == id);
            if(attendance == null)
            {
                throw new KeyNotFoundException($"Attendance record not found for ID {id}.");
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

        public async Task<List<DailyRosterResponse>> GetDailyRoster(DateOnly date, int WorkPeriodNbr)
        {
            var dailyRosterAssignments = await (from ea in _context.EmployeeAssignments
                                                join
                                                wp in _context.WorkPeriods on ea.WorkPeriodId equals wp.WorkPeriodId
                                                join
                                                att in _context.Attendances.Where(att => !att.DeletedInd && att.AttendanceDate == date)
                                                   on ea.EmployeeAssignmentId equals att.EmployeeAssignmentId into attGroup
                                                from attendance in attGroup.DefaultIfEmpty()
                                                where !ea.DeletedInd
                                                && wp.WorkPeriodNbr == WorkPeriodNbr
                                                && ea.AssignmentStartDate <= date
                                                && (ea.AssignmentEndDate == null || ea.AssignmentEndDate >= date)
                                                select new
                                                {
                                                    EmployeeAssignmentId = ea.EmployeeAssignmentId,
                                                    Roic = ea.Roic,
                                                    WorkPeriodNbr = wp.WorkPeriodNbr,
                                                    Attendance = attendance
                                                }).ToListAsync();

            var result = dailyRosterAssignments.Select(item => new DailyRosterResponse
            (
                EmployeeAssignmentId: item.EmployeeAssignmentId,
                Roic: item.Roic,
                WorkPeriodNbr: item.WorkPeriodNbr,
                Attendance: _mapper.Map<AttendanceResponse?>(item.Attendance)
            )).ToList();
            return result;


        }
        public async Task<List<AttendanceStatusResponse>> GetAttendanceStatus()
        {
            return _mapper.Map<List<AttendanceStatusResponse>>(await _context.AttendanceStatuses.ToListAsync());
        }

    }
}
