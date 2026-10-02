namespace FireTime.Dtos.Lookups;

public sealed record LeaveCategoryRequest(string LeaveCategoryCode, string LeaveCategoryDesc);
public sealed record LeaveCategoryResponse(int LeaveCategoryId, string LeaveCategoryCode, string LeaveCategoryDesc);

public sealed record LeaveDetailCodeRequest(string LeaveDetailCode1, string LeaveDetailDesc, int LeaveCategoryId);
public sealed record LeaveDetailCodeResponse(int LeaveDetailId, string LeaveDetailCode1, string LeaveDetailDesc, int LeaveCategoryId);

public sealed record LeaveTransactionTypeRequest(string LeaveTransactionDesc);
public sealed record LeaveTransactionTypeResponse(int LeaveTransactionTypeId, string LeaveTransactionDesc);

public sealed record LeaveDefaultHourRequest(
    decimal DefaultHours,
    int LeaveCategoryId,
    int LeaveTransactionTypeId,
    int ShiftId,
    int LeaveDetailId);

public sealed record LeaveDefaultHourResponse(
    int LeaveDefaultHoursId,
    decimal DefaultHours,
    int LeaveCategoryId,
    int LeaveTransactionTypeId,
    int ShiftId,
    int LeaveDetailId);

public sealed record SickSellbackCodeRequest(
    string SickSellbackCode1,
    string SickSellbackDesc,
    string? SickSellbackCodeComments,
    int ShiftId);

public sealed record SickSellbackCodeResponse(
    int SickSellbackCodeId,
    string SickSellbackCode1,
    string SickSellbackDesc,
    string? SickSellbackCodeComments,
    int ShiftId);
