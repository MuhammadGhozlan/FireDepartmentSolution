using FireTime.Dtos.Leave;
using FireTime.Dtos.Transfers;

namespace FireTime.Interfaces.ServiceInterfaces;

public interface IRequestWorkflowService
{
    Task<LeaveRequestResponse?> GetLeaveAsync(int id, CancellationToken cancellationToken);
    Task<IReadOnlyList<LeaveRequestResponse>> ListLeaveAsync(int offset, int limit, CancellationToken cancellationToken);
    Task<LeaveRequestResponse> CreateLeaveAsync(string requesterRoic, LeaveRequestRequest request, CancellationToken cancellationToken);
    Task<LeaveRequestResponse?> UpdateLeaveAsync(int id, LeaveRequestRequest request, CancellationToken cancellationToken);
    Task<TransferRequestResponse?> GetTransferAsync(int id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TransferRequestResponse>> ListTransferAsync(int offset, int limit, CancellationToken cancellationToken);
    Task<TransferRequestResponse> CreateTransferAsync(string requesterRoic, TransferRequestRequest request, CancellationToken cancellationToken);
    Task<TransferRequestResponse?> UpdateTransferAsync(int id, TransferRequestRequest request, CancellationToken cancellationToken);
}
