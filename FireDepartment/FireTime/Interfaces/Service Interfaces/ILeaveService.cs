using FireTime.Dtos.Leave;

namespace FireTime.Interfaces.Service_Interfaces
{
    public interface ILeaveService
    {
        Task<List<LeaveRequestResponse>> GetAllLeaveRequestsAsync();
        Task<List<LeaveRequestResponse>> GetAllLeaveRequestsForRequesterAsync(string requesterRoic);
        Task<LeaveRequestResponse> GetLeaveRequestByIdAsync(int leaveRequestId);
        Task<LeaveRequestResponse> MakeLeaveRequestsAsync(LeaveRequestRequest request);
        Task<LeaveRequestResponse> UpdateLeaveRequestAsync(int leaveRequestId, LeaveRequestRequest request);
        Task<bool> DeleteLeaveRequestsAsync(int leaveRequestId);
    }
}
