using AutoMapper;
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
            foreach(var attendance in attendanceList)
            {
                attendance.CreatedDate = DateTime.Now;
                attendance.LastUpdateDate = DateTime.Now;
                attendance.CreatedBy = "System";
                attendance.LastUpdateBy = "System";
            }
            await _context.Attendances.AddRangeAsync(attendanceList);               
            await _context.SaveChangesAsync();
            return _mapper.Map<List<AttendanceResponse>>(attendanceList);
        }
        public async Task<AttendanceResponse?> UpdateAttendance(AttendanceRequest attendanceRequest, int id)
        {
            var attendance = await _context.Attendances.Where(att => att.DeletedInd == false).FirstOrDefaultAsync(att => att.AttendanceId == id);
            if(attendance == null)
            {
                return null;
            }
            _mapper.Map(attendanceRequest, attendance);
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
            await _context.SaveChangesAsync();
            return _mapper.Map<AttendanceResponse>(attendance);

        }
        public async Task<List<AttendanceResponse>> FilterAttendance(AttendanceRequest attendanceRequest)
        {

            var query = _context.Attendances.Where(att => !att.DeletedInd);

            if(attendanceRequest != null)
            {

                if(attendanceRequest.AttendanceDate.HasValue && attendanceRequest.AttendanceDate != default)
                {
                    query = query.Where(att => att.AttendanceDate == attendanceRequest.AttendanceDate);
                }
                if(attendanceRequest.StartDate.HasValue)
                {
                    query = query.Where(att => att.AttendanceDate >= attendanceRequest.StartDate.Value);
                }

                if(attendanceRequest.EndDate.HasValue)
                {
                    query = query.Where(att => att.AttendanceDate <= attendanceRequest.EndDate.Value);
                }

                if(!string.IsNullOrWhiteSpace(attendanceRequest.Roic))
                {
                    query = query.Where(att => att.Roic == attendanceRequest.Roic);
                }

                if(attendanceRequest.EmployeeAssignmentId != null && attendanceRequest.EmployeeAssignmentId != 0)
                {
                    query = query.Where(att => att.EmployeeAssignmentId == attendanceRequest.EmployeeAssignmentId);
                }

                if(attendanceRequest.AttendanceStatusId != null && attendanceRequest.AttendanceStatusId != 0)
                {
                    query = query.Where(att => att.AttendanceStatusId == attendanceRequest.AttendanceStatusId);
                }

                if(!string.IsNullOrWhiteSpace(attendanceRequest.AttendanceComments))
                {
                    query = query.Where(att => att.AttendanceComments == attendanceRequest.AttendanceComments);
                }
            }
            var attendances = await query.ToListAsync();
            return _mapper.Map<List<AttendanceResponse>>(attendances);
        }

    }
}
