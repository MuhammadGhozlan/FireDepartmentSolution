using FireTime.Dtos.Employees;

namespace FireTime.Interfaces.Repo_Interfaces
{
    public interface IEmployeeAssignmentRepository
    {       

        Task<List<EmployeeAssignmentResponse>> GetEmployeeAssignments();
        Task<List<EmployeeAssignmentResponse>> GetEmployeeAssignmentsByEmployeeNbr(string roic);
        Task<EmployeeAssignmentResponse> GetEmployeeAssignmentById(EmployeeAssignmentRequest request, int assignmentId);
        Task<EmployeeAssignmentResponse> CreateEmployeeAssignment(EmployeeAssignmentRequest request);
        Task<EmployeeAssignmentResponse> UpdateEmployeeAssignment(EmployeeAssignmentRequest request, int assignmentId);
        Task<bool> DeleteEmployeeAssignment(EmployeeAssignmentRequest request, int assignmentId);

    }
}
