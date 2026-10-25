using FireTime.Dtos.Employees;

namespace FireTime.Interfaces.Service_Interfaces
{
    public interface IEmployeeAssignmentService
    {
        Task<List<EmployeeAssignmentResponse>> GetEmployeeAssignments();
        Task<List<EmployeeAssignmentResponse>> GetEmployeeAssignmentsByEmployeeNbr(string roic);
        Task<EmployeeAssignmentResponse> GetEmployeeAssignmentById(string roic, int assignmentId);
        Task<EmployeeAssignmentResponse> CreateEmployeeAssignment(EmployeeAssignmentRequest request);
        Task<EmployeeAssignmentResponse> UpdateEmployeeAssignment(EmployeeAssignmentRequest request, int assignmentId);
        Task<bool> DeleteEmployeeAssignment(string roic, int assignmentId);
    }
}
