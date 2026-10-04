using FireTime.Interfaces.RepoInterfaces;
using FireTime.Models;
using Microsoft.EntityFrameworkCore;

namespace FireTime.Repositories;

public sealed class AttendanceRepository(IisFireTimeContext context) : IAttendanceRepository
{
    private readonly EfCrudRepository<Attendance, int> _base = new(context);
    public bool SupportsSoftDelete => _base.SupportsSoftDelete;
    public Task<Attendance?> FindAsync(int id, CancellationToken token) => _base.FindAsync(id, token);
    public Task<IReadOnlyList<Attendance>> ListAsync(int offset, int limit, CancellationToken token) => _base.ListAsync(offset, limit, token);
    public Task AddAsync(Attendance entity, CancellationToken token) => _base.AddAsync(entity, token);
    public Task SaveAsync(CancellationToken token) => _base.SaveAsync(token);
    public bool IsDeleted(Attendance entity) => _base.IsDeleted(entity);
    public void MarkDeleted(Attendance entity) => _base.MarkDeleted(entity);
    public void StampCreated(Attendance entity, string actor, DateTime timestamp) => _base.StampCreated(entity, actor, timestamp);
    public void StampUpdated(Attendance entity, string actor, DateTime timestamp) => _base.StampUpdated(entity, actor, timestamp);

    public Task<bool> AssignmentMatchesAsync(int assignmentId, string roic, DateOnly date, CancellationToken token) =>
        context.EmployeeAssignments.AnyAsync(a => a.EmployeeAssignmentId == assignmentId &&
            a.Roic == roic && !a.DeletedInd && a.AssignmentStartDate <= date &&
            (a.AssignmentEndDate == null || a.AssignmentEndDate >= date), token);

    public Task<bool> StatusExistsAsync(int statusId, CancellationToken token) =>
        context.AttendanceStatuses.AnyAsync(s => s.AttendanceStatusId == statusId, token);

    public async Task<IReadOnlyList<Attendance>> ForEmployeeAsync(string roic, DateOnly date, CancellationToken token) =>
        await context.Attendances.AsNoTracking()
            .Where(a => a.Roic == roic && a.AttendanceDate == date && !a.DeletedInd)
            .OrderBy(a => a.AttendanceId)
            .ToListAsync(token);
}
