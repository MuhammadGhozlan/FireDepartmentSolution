namespace FireTime.Dtos.Transfers;

public sealed record TransferRequestRequest(
    int RequesterJobTitleId,
    int ShiftFromId,
    int ShiftToId,
    int StationFromId,
    int StationToId,
    int CompanyFromId,
    int CompanyToId,
    int CompanyPositionFromId,
    int CompanyPositionToId,
    bool HazMatRequested,
    bool HazMatPosition,
    bool ParamedicRequested,
    bool ParamedicPosition,
    bool IsTemp,
    string? TransferRequestComment);

public sealed record TransferRequestResponse(
    int TransferRequestId,
    DateTime TransferRequestDate,
    string RequesterRoic,
    int RequesterJobTitleId,
    int ShiftFromId,
    int ShiftToId,
    int StationFromId,
    int StationToId,
    int CompanyFromId,
    int CompanyToId,
    int CompanyPositionFromId,
    int CompanyPositionToId,
    bool HazMatRequested,
    bool HazMatPosition,
    bool ParamedicRequested,
    bool ParamedicPosition,
    bool IsTemp,
    string? TransferRequestComment,
    string? ApproverRoic,
    int ApprovalStatusId,
    string? ApproverComment);

public sealed record TransferMileageRequest(
    string Roic,
    int StationDistanceId,
    DateTime TransferDateTime,
    DateTime ScheduledReportDate);

public sealed record TransferMileageResponse(
    int TransferMileageId,
    string Roic,
    int StationDistanceId,
    DateTime TransferDateTime,
    DateTime ScheduledReportDate);
