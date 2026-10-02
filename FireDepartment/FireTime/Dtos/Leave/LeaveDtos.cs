namespace FireTime.Dtos.Leave;

public sealed record LeaveRequestRequest(
    decimal LeaveRequestHours,
    DateTime LeaveStartDate,
    DateTime LeaveEndDate,
    int LeaveDetailId,
    string? RequesterComment);

public sealed record LeaveRequestResponse(
    int LeaveRequestId,
    DateTime LeaveRequestSubmitDate,
    decimal LeaveRequestHours,
    DateTime LeaveStartDate,
    DateTime LeaveEndDate,
    string RequesterRoic,
    int LeaveDetailId,
    string? ApproverRoic,
    int ApprovalStatusId,
    string? RequesterComment,
    string? ApproverComment);

public sealed record LeaveTransactionRequest(
    string Roic,
    decimal LeaveHours,
    DateOnly LeaveTransactionDate,
    int LeaveDetailId,
    int LeaveTransactionTypeId,
    string? LeaveTransactionComments);

public sealed record LeaveTransactionResponse(
    int LeaveTransactionId,
    string Roic,
    decimal LeaveHours,
    DateOnly LeaveTransactionDate,
    int LeaveDetailId,
    int LeaveTransactionTypeId,
    string? LeaveTransactionComments);
