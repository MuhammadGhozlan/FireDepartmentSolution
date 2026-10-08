using FireTime.Dtos.Attendance;

namespace FireTime.Interfaces.Service_Interfaces
{
    public interface IAttendanceService
    {
        Task<List<AttendanceResponse>> GetAllAttendance();
        Task<List<AttendanceResponse>> TakeAttendance(List<AttendanceRequest> attendanceRequestList);
        Task<AttendanceResponse> UpdateAttendance(UpdateAttendanceRequest updateattendanceRequest, int id);
        Task<AttendanceResponse> DeleteAttendance(int id);
        Task<List<AttendanceResponse>> FilterAttendance(AttendanceFilterRequest attendanceFilterRequest);
    }
}
