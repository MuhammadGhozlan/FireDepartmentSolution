using FireTime.Dtos.Employees;
using FireTime.Interfaces.Repo_Interfaces;

namespace FireTime.Interfaces.Service_Interfaces
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeDetailResponse>> GetEmployees()
        {
            return await _employeeRepository.GetEmployees();
        }

        public async Task<EmployeeDetailResponse?> GetEmployeeByRoic(string roic)
        {
            return await _employeeRepository.GetEmployeeByRoic(roic);
        }

        public async Task<EmployeeDetailResponse> CreateEmployee(CreateEmployeeRequest employee)
        {
            return await _employeeRepository.CreateEmployee(employee);
        }

        public async Task<EmployeeDetailResponse> UpdateEmployee(UpdateEmployeeRequest employee, string roic)
        {
            return await _employeeRepository.UpdateEmployee(employee, roic);
        }
    }
}
