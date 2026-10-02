namespace FireTime.Dtos.Lookups;

public sealed record EmployeeStatusCodeRequest(string EmployeeStatusCde, string EmployeeStatusDesc);
public sealed record EmployeeStatusCodeResponse(string EmployeeStatusCde, string EmployeeStatusDesc);

public sealed record AttendanceStatusRequest(string AttendanceStatusCde, string AttendanceStatusDesc);
public sealed record AttendanceStatusResponse(int AttendanceStatusId, string AttendanceStatusCde, string AttendanceStatusDesc);

public sealed record ApprovalStatusRequest(string ApprovalStatusCde);
public sealed record ApprovalStatusResponse(int ApprovalStatusId, string ApprovalStatusCde);

public sealed record ContactTypeRequest(string ContactTypeDesc);
public sealed record ContactTypeResponse(int ContactTypeId, string ContactTypeDesc);

public sealed record DistrictRequest(string DistrictNme, int? StationCount, bool IsActive, string? DistrictComment);
public sealed record DistrictResponse(int DistrictId, string DistrictNme, int? StationCount, bool IsActive, string? DistrictComment);

public sealed record StationRequest(string StationNbr, bool IsActive, string? StationComment, int DistrictId);
public sealed record StationResponse(int StationId, string StationNbr, bool IsActive, string? StationComment, int DistrictId);

public sealed record CompanyRequest(
    string CompanyNme,
    bool IsActive,
    int CompanyMemberCount,
    string? MedicalClassification,
    string? CompanyComment,
    int? StationId);

public sealed record CompanyResponse(
    int CompanyId,
    string CompanyNme,
    bool IsActive,
    int CompanyMemberCount,
    string? MedicalClassification,
    string? CompanyComment,
    int? StationId);

public sealed record CompanyPositionRequest(byte TruckPosition, int CompanyId, int JobTitleId, int ShiftId);
public sealed record CompanyPositionResponse(int CompanyPositionId, byte TruckPosition, int CompanyId, int JobTitleId, int ShiftId);

public sealed record ShiftRequest(string ShiftCode, string? ShiftNme, byte? SortOrder, bool IsActive);
public sealed record ShiftResponse(int ShiftId, string ShiftCode, string? ShiftNme, byte? SortOrder, bool IsActive);

public sealed record JobTitleRequest(string JobTitleDesc, string JobTitleShortDesc, bool IsActive, string SalaryRangeGradeCde);
public sealed record JobTitleResponse(int JobTitleId, string JobTitleDesc, string JobTitleShortDesc, bool IsActive, string SalaryRangeGradeCde);

public sealed record WorkPeriodRequest(string WeekDay, int WorkPeriodNbr);
public sealed record WorkPeriodResponse(int WorkPeriodId, string WeekDay, int WorkPeriodNbr);

public sealed record StationDistanceRequest(decimal DistanceMiles, int FromStationId, int ToStationId);
public sealed record StationDistanceResponse(int StationDistanceId, decimal DistanceMiles, int FromStationId, int ToStationId);
