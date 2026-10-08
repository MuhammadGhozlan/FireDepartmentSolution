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
            foreach(var attendance in attendanceList)
            {
                var assignmentId=_context.EmployeeAssignments.Where(ea => ea.DeletedInd == false).FirstOrDefault(ea => ea.Roic == attendance.Roic)?.EmployeeAssignmentId;
                attendance.EmployeeAssignmentId= assignmentId ?? throw new Exception($"EmployeeAssignmentId {attendance.EmployeeAssignmentId} does not exist or is deleted.");
                attendance.CreatedDate = now;
                attendance.LastUpdateDate = now;
                attendance.CreatedBy = "System";
                attendance.LastUpdateBy = "System";
                
                
            }
            await _context.Attendances.AddRangeAsync(attendanceList);
            await _context.SaveChangesAsync();
            return _mapper.Map<List<AttendanceResponse>>(attendanceList);
        }
        public async Task<AttendanceResponse?> UpdateAttendance(UpdateAttendanceRequest updateAttendanceRequest, int id)
        {
            var attendance = await _context.Attendances.Where(att => att.DeletedInd == false).FirstOrDefaultAsync(att => att.AttendanceId == id);
            if(attendance == null)
            {
                return null;
            }
            if(updateAttendanceRequest.AttendanceDate.HasValue)
                attendance.AttendanceDate = updateAttendanceRequest.AttendanceDate.Value;

            if(!string.IsNullOrWhiteSpace(updateAttendanceRequest.Roic))
                attendance.Roic = updateAttendanceRequest.Roic;

            if(updateAttendanceRequest.EmployeeAssignmentId.HasValue)
                attendance.EmployeeAssignmentId = updateAttendanceRequest.EmployeeAssignmentId.Value;

            if(updateAttendanceRequest.AttendanceStatusId.HasValue)
                attendance.AttendanceStatusId = updateAttendanceRequest.AttendanceStatusId.Value;

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
