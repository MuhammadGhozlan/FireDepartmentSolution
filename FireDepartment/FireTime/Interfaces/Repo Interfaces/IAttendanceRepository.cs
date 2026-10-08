
using FireTime.Dtos.Attendance;

namespace FireTime.Interfaces.Repo_Interfaces
{
    public interface IAttendanceRepository
    {
        Task<List<AttendanceResponse>> GetAllAttendance();
        Task<List<AttendanceResponse>> TakeAttendance(List<AttendanceRequest> attendanceRequestList);
        Task<AttendanceResponse?> UpdateAttendance(UpdateAttendanceRequest updateAttendanceRequest, int id);
        Task<AttendanceResponse?> DeleteAttendance(int id);
        Task<List<AttendanceResponse>> FilterAttendance(AttendanceFilterRequest attendanceFilterRequest);
    }
}
