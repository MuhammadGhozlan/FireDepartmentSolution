using FireTime.Dtos.Leave;

namespace FireTime.Interfaces.Repo_Interfaces
{
    public interface ILeaveRepository
    {
        Task<List<LeaveRequestResponse>> GetAllLeaveRequests();
        Task<LeaveRequestResponse> GetLeaveRequestById(int leaveRequestId);
        Task<List<LeaveRequestResponse>> GetAllLeaveRequestsForRequester(string requesterRoic);
        Task<LeaveRequestResponse> MakeLeaveRequests(LeaveRequestRequest request);
        Task<bool> DeleteLeaveRequests(int leaveRequestId);
        Task<LeaveRequestResponse> UpdateLeaveRequest(int leaveRequestId, LeaveRequestRequest request);
    }
}
