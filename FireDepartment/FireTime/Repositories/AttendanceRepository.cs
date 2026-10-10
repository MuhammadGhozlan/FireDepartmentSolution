using AutoMapper;
using Azure.Core;
using FireTime.Dtos.Attendance;
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

        public async Task<List<AttendanceResponse>> TakeAttendance(List<AttendanceRequest> attendanceRequestList)
        {
            var attendanceList = _mapper.Map<List<Attendance>>(attendanceRequestList);
            DateTime now = DateTime.Now;

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

                attendance.CreatedDate = now;
                attendance.LastUpdateDate = now;
                attendance.CreatedBy = "System";
                attendance.LastUpdateBy = "System";
                attendance.DeletedInd = false;
            }

            await _context.Attendances.AddRangeAsync(attendanceList);
            await _context.SaveChangesAsync();
            return _mapper.Map<List<AttendanceResponse>>(attendanceList);
        }

        public async Task<AttendanceResponse?> UpdateAttendance(UpdateAttendanceRequest updateAttendanceRequest, int id)
        {
            var attendance = await _context.Attendances
                .Where(att => !att.DeletedInd)
                .FirstOrDefaultAsync(att => att.AttendanceId == id);

            if(attendance == null)
                return null;

            var targetRoic = !string.IsNullOrWhiteSpace(updateAttendanceRequest.Roic)
                ? updateAttendanceRequest.Roic
                : attendance.Roic;

            var targetDate = updateAttendanceRequest.AttendanceDate ?? attendance.AttendanceDate;

            if(targetRoic != attendance.Roic || updateAttendanceRequest.AttendanceDate.HasValue)
            {
                var assignment = await _context.EmployeeAssignments
                    .Where(ea => !ea.DeletedInd && ea.Roic == targetRoic)
                    .FirstOrDefaultAsync(ea => ea.AssignmentStartDate <= targetDate
                                            && (ea.AssignmentEndDate == null || ea.AssignmentEndDate >= targetDate));

                attendance.EmployeeAssignmentId = assignment?.EmployeeAssignmentId
                    ?? throw new KeyNotFoundException($"Active assignment not found for ROIC {targetRoic} on {targetDate}.");

                attendance.Roic = targetRoic;
                attendance.AttendanceDate = targetDate;
            }

            if(updateAttendanceRequest.AttendanceComments != null)
                attendance.AttendanceComments = string.IsNullOrWhiteSpace(updateAttendanceRequest.AttendanceComments)
                    ? null
                    : updateAttendanceRequest.AttendanceComments;

            if(updateAttendanceRequest.AttendanceComments != null)
                attendance.AttendanceComments = updateAttendanceRequest.AttendanceComments;

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
