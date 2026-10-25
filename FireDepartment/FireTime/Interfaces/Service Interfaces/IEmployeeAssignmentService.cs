using FireTime.Dtos.Employees;

namespace FireTime.Interfaces.Service_Interfaces
{
    public interface IEmployeeAssignmentService
    {
        Task<List<EmployeeAssignmentResponse>> GetEmployeeAssignments();
        Task<List<EmployeeAssignmentResponse>> GetEmployeeAssignmentsByEmployeeNbr(string roic);
        Task<EmployeeAssignmentResponse> GetEmployeeAssignmentById(EmployeeAssignmentRequest request, int assignmentId);
        Task<EmployeeAssignmentResponse> CreateEmployeeAssignment(EmployeeAssignmentRequest request);
        Task<EmployeeAssignmentResponse> UpdateEmployeeAssignment(EmployeeAssignmentRequest request, int assignmentId);
        Task<bool> DeleteEmployeeAssignment(EmployeeAssignmentRequest request, int assignmentId);
    }
}
