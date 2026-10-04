using FireTime.Dtos.Attendance;

namespace FireTime.Interfaces.ServiceInterfaces;

public interface IAttendanceService : ICrudService<AttendanceRequest, AttendanceResponse, int>
{
    Task<IReadOnlyList<AttendanceResponse>> ForEmployeeAsync(string roic, DateOnly date, CancellationToken cancellationToken);
}
