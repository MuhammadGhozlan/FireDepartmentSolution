using FireTime.Dtos.Employees;
using FireTime.Interfaces.Repo_Interfaces;
using FireTime.Interfaces.Service_Interfaces;

namespace FireTime.Services
{
    public class EmployeeAssignmentService : IEmployeeAssignmentService
    {
        private readonly IEmployeeAssignmentRepository _repository;

        public EmployeeAssignmentService(IEmployeeAssignmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<EmployeeAssignmentResponse>> GetEmployeeAssignments() =>
            await _repository.GetEmployeeAssignments();

        public async Task<List<EmployeeAssignmentResponse>> GetEmployeeAssignmentsByEmployeeNbr(string roic) =>
            await _repository.GetEmployeeAssignmentsByEmployeeNbr(roic);

        public async Task<EmployeeAssignmentResponse> GetEmployeeAssignmentById(string roic, int assignmentId) =>
            await _repository.GetEmployeeAssignmentById(roic, assignmentId);

        public async Task<EmployeeAssignmentResponse> CreateEmployeeAssignment(EmployeeAssignmentRequest request) =>
            await _repository.CreateEmployeeAssignment(request);

        public async Task<EmployeeAssignmentResponse> UpdateEmployeeAssignment(EmployeeAssignmentRequest request, int assignmentId) =>
            await _repository.UpdateEmployeeAssignment(request, assignmentId);

        public async Task<bool> DeleteEmployeeAssignment(EmployeeAssignmentRequest request, int assignmentId) =>
            await _repository.DeleteEmployeeAssignment(request, assignmentId);
    }
}
