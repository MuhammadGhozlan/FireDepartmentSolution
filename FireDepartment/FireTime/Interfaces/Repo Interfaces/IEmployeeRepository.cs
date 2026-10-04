using FireTime.Models;

namespace FireTime.Interfaces.RepoInterfaces;

public interface IEmployeeRepository : ICrudRepository<Employee, string>
{
    Task<bool> StatusExistsAsync(string code, CancellationToken cancellationToken);
}
