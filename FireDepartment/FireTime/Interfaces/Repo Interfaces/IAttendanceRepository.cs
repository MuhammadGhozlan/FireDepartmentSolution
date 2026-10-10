
using FireTime.Dtos.Attendance;

using FireTime.Dtos.Lookups;

namespace FireTime.Interfaces.Repo_Interfaces
{
    public interface IAttendanceRepository
    {
        Task<List<AttendanceResponse>> GetAllAttendance();
        Task<List<AttendanceResponse>> TakeAttendance(List<AttendanceRequest> attendanceRequestList);
        Task<AttendanceResponse?> UpdateAttendance(UpdateAttendanceRequest updateAttendanceRequest, int id);
        Task<AttendanceResponse?> DeleteAttendance(int id);
        Task<List<AttendanceResponse>> FilterAttendance(AttendanceFilterRequest attendanceFilterRequest);
        Task<List<AttendanceStatusResponse>> GetAttendanceStatuses();
        Task<List<AttendanceAssignmentResponse>> GetAttendanceAssignments(string roic, DateOnly date);
        Task<List<AttendanceRosterResponse>> GetAttendanceRoster(DateOnly date, int workPeriodNbr);
    }
}
