using FireTime.Interfaces.RepoInterfaces;
using FireTime.Models;
using Microsoft.EntityFrameworkCore;

namespace FireTime.Repositories;

public sealed class WorkflowLookupRepository(IisFireTimeContext context) : IWorkflowLookupRepository
{
    public Task<bool> EmployeeExistsAsync(string roic, CancellationToken token) =>
        context.Employees.AnyAsync(e => e.Roic == roic, token);

    public Task<int?> PendingStatusIdAsync(string code, CancellationToken token) =>
        context.ApprovalStatuses.Where(s => s.ApprovalStatusCde == code)
            .Select(s => (int?)s.ApprovalStatusId).FirstOrDefaultAsync(token);
}
