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

        public async Task<List<AttendanceResponse>> TakeAttendance(List<AttendanceRequest> attendanceRequestList)
        {

            var attendanceList = new List<Attendance>();
            foreach(var req in attendanceRequestList)
            {

                var employeeAssignmentIds = await _context.EmployeeAssignments
                                                  .Where(ea => ea.Roic == req.Roic &&
                                                               ea.AssignmentStartDate <= req.AttendanceDate &&
                                                               (ea.AssignmentEndDate == null || ea.AssignmentEndDate >= req.AttendanceDate) &&
                                                               !ea.DeletedInd)
                                                  .Select(ea => ea.EmployeeAssignmentId).ToListAsync();
                if(!employeeAssignmentIds.Any())
                {
                    throw new KeyNotFoundException($"Active assignment not found for ROIC {req.Roic} on {req.AttendanceDate}.");
                }
                foreach(var employeeAssignmentId in employeeAssignmentIds)
                {
                    attendanceList.Add(new Attendance
                    {
                        AttendanceDate = req.AttendanceDate,
                        Roic = req.Roic,
                        AttendanceStatusId = req.AttendanceStatusId,
                        AttendanceComments = req.AttendanceComments,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "System",
                        LastUpdateDate = DateTime.Now,
                        LastUpdateBy = "System",
                        DeletedInd = false,
                        EmployeeAssignmentId = employeeAssignmentId
                    });
                }

            }
            await _context.Attendances.AddRangeAsync(attendanceList);
            await _context.SaveChangesAsync();
            var attendanceResponseList = _mapper.Map<List<AttendanceResponse>>(attendanceList);
            return attendanceResponseList;

        }

        public async Task<AttendanceResponse?> UpdateAttendance(UpdateAttendanceRequest updateAttendanceRequest, int id)
        {
            var attendance = await _context.Attendances.Where(att => att.DeletedInd == false && att.AttendanceId == id).FirstOrDefaultAsync();
            if(attendance == null)
            {
                throw new KeyNotFoundException($"Attendance record with ID {id} not found");
            }
            if(updateAttendanceRequest.AttendanceDate.HasValue && updateAttendanceRequest.AttendanceDate.Value != default)
            {
                attendance.AttendanceDate = updateAttendanceRequest.AttendanceDate.Value;
                attendance.EmployeeAssignmentId = await _context.EmployeeAssignments
                    .Where(ea => ea.Roic == attendance.Roic &&
                                 ea.AssignmentStartDate <= updateAttendanceRequest.AttendanceDate.Value &&
                                 (ea.AssignmentEndDate == null || ea.AssignmentEndDate >= updateAttendanceRequest.AttendanceDate.Value) &&
                                 !ea.DeletedInd)
                    .Select(ea => ea.EmployeeAssignmentId)
                    .FirstOrDefaultAsync();
            }
            if(updateAttendanceRequest.AttendanceStatusId.HasValue && updateAttendanceRequest.AttendanceStatusId.Value != 0)
            {
                attendance.AttendanceStatusId = updateAttendanceRequest.AttendanceStatusId.Value;
            }
            if(updateAttendanceRequest.AttendanceComments != null)
            {
                attendance.AttendanceComments = updateAttendanceRequest.AttendanceComments;
            }
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
