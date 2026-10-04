using FireTime.Interfaces.RepoInterfaces;
using FireTime.Models;
using Microsoft.EntityFrameworkCore;

namespace FireTime.Repositories;

public sealed class EmployeeRepository(IisFireTimeContext context) : IEmployeeRepository
{
    private readonly EfCrudRepository<Employee, string> _base = new(context);
    public bool SupportsSoftDelete => false;
    public Task<Employee?> FindAsync(string id, CancellationToken token) => _base.FindAsync(id, token);
    public Task<IReadOnlyList<Employee>> ListAsync(int offset, int limit, CancellationToken token) => _base.ListAsync(offset, limit, token);
    public Task AddAsync(Employee entity, CancellationToken token) => _base.AddAsync(entity, token);
    public Task SaveAsync(CancellationToken token) => _base.SaveAsync(token);
    public bool IsDeleted(Employee entity) => false;
    public void MarkDeleted(Employee entity) => throw new NotSupportedException();
    public void StampCreated(Employee entity, string actor, DateTime timestamp) => _base.StampCreated(entity, actor, timestamp);
    public void StampUpdated(Employee entity, string actor, DateTime timestamp) => _base.StampUpdated(entity, actor, timestamp);
    public Task<bool> StatusExistsAsync(string code, CancellationToken token) =>
        context.EmployeeStatusCodes.AnyAsync(s => s.EmployeeStatusCde == code, token);
}
