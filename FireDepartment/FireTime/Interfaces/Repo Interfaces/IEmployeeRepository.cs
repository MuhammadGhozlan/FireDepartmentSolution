using FireTime.Dtos.Employees;

namespace FireTime.Interfaces.Repo_Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeDetailResponse>> GetEmployees();
        Task<EmployeeDetailResponse?> GetEmployeeByRoic(string roic);
        Task<EmployeeDetailResponse> CreateEmployee(CreateEmployeeRequest employee);
        Task<EmployeeDetailResponse> UpdateEmployee(UpdateEmployeeRequest employee, string roic);
    }
}
