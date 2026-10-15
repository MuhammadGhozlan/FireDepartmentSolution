
using FireTime.Dtos.Attendance;
using FireTime.Dtos.Lookups;

namespace FireTime.Interfaces.Repo_Interfaces
{
    public interface IAttendanceRepository
    {
        Task<List<AttendanceResponse>> GetAllAttendance();
        Task<List<AttendanceResponse>?> TakeAttendance(List<AttendanceRequest> attendanceRequestList);
        Task<AttendanceResponse?> UpdateAttendance(UpdateAttendanceRequest updateAttendanceRequest, int id);
        Task<AttendanceResponse?> DeleteAttendance(int id);
        Task<List<AttendanceResponse>> FilterAttendance(AttendanceFilterRequest attendanceFilterRequest);
        Task<List<DailyRosterResponse>> GetDailyRoster(DateOnly date, int WorkPeriodNbr);
        Task<List<AttendanceStatusResponse>> GetAttendanceStatus();
    }
}
