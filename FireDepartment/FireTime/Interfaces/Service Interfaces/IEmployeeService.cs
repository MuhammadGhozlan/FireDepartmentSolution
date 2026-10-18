using FireTime.Dtos.Employees;

namespace FireTime.Interfaces.Service_Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDetailResponse>> GetEmployees();
        Task<EmployeeDetailResponse?> GetEmployeeByRoic(string roic);
        Task<EmployeeDetailResponse> CreateEmployee(CreateEmployeeRequest employee);
        Task<EmployeeDetailResponse> UpdateEmployee(UpdateEmployeeRequest employee, string roic);
    }
}
