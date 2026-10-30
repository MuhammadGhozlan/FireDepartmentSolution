using FireTime.Dtos.Leave;
using FireTime.Interfaces.Repo_Interfaces;
using FireTime.Interfaces.Service_Interfaces;

namespace FireTime.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly ILeaveRepository _leaveRepository;

        public LeaveService(ILeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }

        public async Task<List<LeaveRequestResponse>> GetAllLeaveRequestsAsync()
        {
            return await _leaveRepository.GetAllLeaveRequests();
        }

        public async Task<List<LeaveRequestResponse>> GetAllLeaveRequestsForRequesterAsync(string requesterRoic)
        {
            if(string.IsNullOrWhiteSpace(requesterRoic))
            {
                throw new ArgumentException("Requester ROIC cannot be null or empty.", nameof(requesterRoic));
            }

            return await _leaveRepository.GetAllLeaveRequestsForRequester(requesterRoic);
        }

        public async Task<LeaveRequestResponse> GetLeaveRequestByIdAsync(int leaveRequestId)
        {
            if(leaveRequestId <= 0)
            {
                throw new ArgumentException("Leave request ID must be greater than zero.", nameof(leaveRequestId));
            }

            return await _leaveRepository.GetLeaveRequestById(leaveRequestId);
        }

        public async Task<LeaveRequestResponse> MakeLeaveRequestsAsync(LeaveRequestRequest request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request), "The leave request payload cannot be null.");
            }

            return await _leaveRepository.MakeLeaveRequests(request);
        }

        public async Task<LeaveRequestResponse> UpdateLeaveRequestAsync(int leaveRequestId, LeaveRequestRequest request)
        {
            if(leaveRequestId <= 0)
            {
                throw new ArgumentException("Leave request ID must be greater than zero.", nameof(leaveRequestId));
            }

            if(request == null)
            {
                throw new ArgumentNullException(nameof(request), "The leave request payload cannot be null.");
            }

            return await _leaveRepository.UpdateLeaveRequest(leaveRequestId, request);
        }

        public async Task<bool> DeleteLeaveRequestsAsync(int leaveRequestId)
        {
            if(leaveRequestId <= 0)
            {
                throw new ArgumentException("Leave request ID must be greater than zero.", nameof(leaveRequestId));
            }

            return await _leaveRepository.DeleteLeaveRequests(leaveRequestId);
        }

    }
}
