using FireTime.Dtos.Employees;

namespace FireTime.Interfaces.ServiceInterfaces;

public interface IEmployeeService
{
    Task<IReadOnlyList<EmployeeSummaryResponse>> ListAsync(int offset, int limit, CancellationToken cancellationToken);
    Task<EmployeeDetailResponse?> GetAsync(string roic, CancellationToken cancellationToken);
    Task<EmployeeDetailResponse> CreateAsync(CreateEmployeeRequest request, CancellationToken cancellationToken);
    Task<EmployeeDetailResponse?> UpdateAsync(string roic, UpdateEmployeeRequest request, CancellationToken cancellationToken);
}
