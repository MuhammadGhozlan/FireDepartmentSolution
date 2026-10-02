namespace FireTime.Dtos.SickSellback;

public sealed record SickSellbackSelectionRequest(
    string Roic,
    int SellbackSelectionYear,
    int SickSellbackCodeId,
    int ShiftId);

public sealed record SickSellbackSelectionResponse(
    int SickSellbackSelectionId,
    string Roic,
    int SellbackSelectionYear,
    DateTime SellbackSelectionDateTime,
    int SickSellbackCodeId,
    int ShiftId);

public sealed record SickSellbackPayoutRequest(
    string Roic,
    int SickSellbackSelectionId,
    int SellbackPayoutYear,
    decimal SellbackPayoutHours,
    short PayTypeCde);

public sealed record SickSellbackPayoutResponse(
    int SickSellbackPayoutId,
    string Roic,
    int SickSellbackSelectionId,
    int SellbackPayoutYear,
    decimal SellbackPayoutHours,
    short PayTypeCde,
    DateTime? PayrollExportDateTime);
