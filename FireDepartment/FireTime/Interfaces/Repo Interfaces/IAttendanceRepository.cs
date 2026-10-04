using FireTime.Models;

namespace FireTime.Interfaces.RepoInterfaces;

public interface IAttendanceRepository : ICrudRepository<Attendance, int>
{
    Task<bool> AssignmentMatchesAsync(int assignmentId, string roic, DateOnly date, CancellationToken cancellationToken);
    Task<bool> StatusExistsAsync(int statusId, CancellationToken cancellationToken);
    Task<IReadOnlyList<Attendance>> ForEmployeeAsync(string roic, DateOnly date, CancellationToken cancellationToken);
}
