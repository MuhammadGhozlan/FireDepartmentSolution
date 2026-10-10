using FireTime.Dtos.Leave;
using FireTime.Dtos.Transfers;
using FireTime.Interfaces.RepoInterfaces;
using FireTime.Interfaces.ServiceInterfaces;
using FireTime.Models;

namespace FireTime.Services;

public sealed class RequestWorkflowService(
    ICrudRepository<LeaveRequest, int> leaves,
    ICrudRepository<TransferRequest, int> transfers,
    IWorkflowLookupRepository lookups,
    IHttpContextAccessor accessor,
    IConfiguration configuration) : IRequestWorkflowService
{
    private string Actor => accessor.HttpContext?.User.Identity?.Name ?? "local-development";
    private string PendingCode => configuration["Workflow:PendingApprovalStatusCode"] ?? "Pending";

    private async Task<int> PendingIdAsync(CancellationToken token) =>
        await lookups.PendingStatusIdAsync(PendingCode, token)
        ?? throw new ArgumentException($"Approval status '{PendingCode}' does not exist. Configure Workflow:PendingApprovalStatusCode.");

    private async Task ValidateRequesterAsync(string roic, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(roic) || !await lookups.EmployeeExistsAsync(roic, token))
            throw new ArgumentException("Requester ROIC does not identify an employee.");
    }

    public async Task<LeaveRequestResponse?> GetLeaveAsync(int id, CancellationToken token)
    {
        var entity = await leaves.FindAsync(id, token);
        return entity is null || leaves.IsDeleted(entity) ? null : MapLeave(entity);
    }

    public async Task<IReadOnlyList<LeaveRequestResponse>> ListLeaveAsync(int offset, int limit, CancellationToken token) =>
        (await leaves.ListAsync(offset, limit, token)).Select(MapLeave).ToArray();

    public async Task<LeaveRequestResponse> CreateLeaveAsync(string requesterRoic, LeaveRequestRequest request, CancellationToken token)
    {
        await ValidateRequesterAsync(requesterRoic, token);
        ValidateLeave(request);
        int pending = await PendingIdAsync(token);
        var entity = new LeaveRequest
        {
            RequesterRoic = requesterRoic,
            ApprovalStatusId = pending,
            LeaveRequestSubmitDate = DateTime.UtcNow,
            LeaveRequestHours = request.LeaveRequestHours,
            LeaveStartDate = request.LeaveStartDate,
            LeaveEndDate = request.LeaveEndDate,
            LeaveDetailId = request.LeaveDetailId,
            RequesterComment = request.RequesterComment
        };
        leaves.StampCreated(entity, Actor, DateTime.UtcNow);
        await leaves.AddAsync(entity, token);
        await leaves.SaveAsync(token);
        return MapLeave(entity);
    }

    public async Task<LeaveRequestResponse?> UpdateLeaveAsync(int id, LeaveRequestRequest request, CancellationToken token)
    {
        var entity = await leaves.FindAsync(id, token);
        if (entity is null || leaves.IsDeleted(entity))
            return null;
        if (entity.ApprovalStatusId != await PendingIdAsync(token))
            throw new InvalidOperationException("Only pending requests can be edited.");
        ValidateLeave(request);
        entity.LeaveRequestHours = request.LeaveRequestHours;
        entity.LeaveStartDate = request.LeaveStartDate;
        entity.LeaveEndDate = request.LeaveEndDate;
        entity.LeaveDetailId = request.LeaveDetailId;
        entity.RequesterComment = request.RequesterComment;
        leaves.StampUpdated(entity, Actor, DateTime.UtcNow);
        await leaves.SaveAsync(token);
        return MapLeave(entity);
    }

    public async Task<TransferRequestResponse?> GetTransferAsync(int id, CancellationToken token)
    {
        var entity = await transfers.FindAsync(id, token);
        return entity is null || transfers.IsDeleted(entity) ? null : MapTransfer(entity);
    }

    public async Task<IReadOnlyList<TransferRequestResponse>> ListTransferAsync(int offset, int limit, CancellationToken token) =>
        (await transfers.ListAsync(offset, limit, token)).Select(MapTransfer).ToArray();

    public async Task<TransferRequestResponse> CreateTransferAsync(string requesterRoic, TransferRequestRequest request, CancellationToken token)
    {
        await ValidateRequesterAsync(requesterRoic, token);
        int pending = await PendingIdAsync(token);
        var entity = new TransferRequest
        {
            RequesterRoic = requesterRoic,
            ApprovalStatusId = pending,
            TransferRequestDate = DateTime.UtcNow,
            RequesterJobTitleId = request.RequesterJobTitleId,
            ShiftFromId = request.ShiftFromId,
            ShiftToId = request.ShiftToId,
            StationFromId = request.StationFromId,
            StationToId = request.StationToId,
            CompanyFromId = request.CompanyFromId,
            CompanyToId = request.CompanyToId,
            CompanyPositionFromId = request.CompanyPositionFromId,
            CompanyPositionToId = request.CompanyPositionToId,
            HazMatRequested = request.HazMatRequested,
            HazMatPosition = request.HazMatPosition,
            ParamedicRequested = request.ParamedicRequested,
            ParamedicPosition = request.ParamedicPosition,
            IsTemp = request.IsTemp,
            TransferRequestComment = request.TransferRequestComment
        };
        transfers.StampCreated(entity, Actor, DateTime.UtcNow);
        await transfers.AddAsync(entity, token);
        await transfers.SaveAsync(token);
        return MapTransfer(entity);
    }

    public async Task<TransferRequestResponse?> UpdateTransferAsync(int id, TransferRequestRequest request, CancellationToken token)
    {
        var entity = await transfers.FindAsync(id, token);
        if (entity is null || transfers.IsDeleted(entity))
            return null;
        if (entity.ApprovalStatusId != await PendingIdAsync(token))
            throw new InvalidOperationException("Only pending requests can be edited.");
        entity.RequesterJobTitleId = request.RequesterJobTitleId;
        entity.ShiftFromId = request.ShiftFromId;
        entity.ShiftToId = request.ShiftToId;
        entity.StationFromId = request.StationFromId;
        entity.StationToId = request.StationToId;
        entity.CompanyFromId = request.CompanyFromId;
        entity.CompanyToId = request.CompanyToId;
        entity.CompanyPositionFromId = request.CompanyPositionFromId;
        entity.CompanyPositionToId = request.CompanyPositionToId;
        entity.HazMatRequested = request.HazMatRequested;
        entity.HazMatPosition = request.HazMatPosition;
        entity.ParamedicRequested = request.ParamedicRequested;
        entity.ParamedicPosition = request.ParamedicPosition;
        entity.IsTemp = request.IsTemp;
        entity.TransferRequestComment = request.TransferRequestComment;
        transfers.StampUpdated(entity, Actor, DateTime.UtcNow);
        await transfers.SaveAsync(token);
        return MapTransfer(entity);
    }

    private static void ValidateLeave(LeaveRequestRequest request)
    {
        if (request.LeaveRequestHours <= 0 || request.LeaveEndDate < request.LeaveStartDate)
            throw new ArgumentException("Leave hours must be positive and the end must not precede the start.");
    }

    private static LeaveRequestResponse MapLeave(LeaveRequest entity) =>
        new(entity.LeaveRequestId, entity.LeaveRequestSubmitDate, entity.LeaveRequestHours,
            entity.LeaveStartDate, entity.LeaveEndDate, entity.RequesterRoic, entity.LeaveDetailId,
            entity.ApproverRoic, entity.ApprovalStatusId, entity.RequesterComment, entity.ApproverComment);

    private static TransferRequestResponse MapTransfer(TransferRequest entity) =>
        new(entity.TransferRequestId, entity.TransferRequestDate, entity.RequesterRoic,
            entity.RequesterJobTitleId, entity.ShiftFromId, entity.ShiftToId, entity.StationFromId,
            entity.StationToId, entity.CompanyFromId, entity.CompanyToId,
            entity.CompanyPositionFromId, entity.CompanyPositionToId, entity.HazMatRequested,
            entity.HazMatPosition, entity.ParamedicRequested, entity.ParamedicPosition,
            entity.IsTemp, entity.TransferRequestComment, entity.ApproverRoic,
            entity.ApprovalStatusId, entity.ApproverComment);
}
