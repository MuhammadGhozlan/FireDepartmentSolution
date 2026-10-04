namespace FireTime.Interfaces.RepoInterfaces;

public interface IWorkflowLookupRepository
{
    Task<bool> EmployeeExistsAsync(string roic, CancellationToken cancellationToken);
    Task<int?> PendingStatusIdAsync(string code, CancellationToken cancellationToken);
}
