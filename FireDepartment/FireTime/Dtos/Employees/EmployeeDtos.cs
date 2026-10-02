namespace FireTime.Dtos.Employees;

public sealed record CreateEmployeeRequest(
    string Roic,
    int EmployeeNbr,
    string FirstNme,
    string? MiddleInitialNme,
    string LastNme,
    string? Suffix,
    bool Paramedic,
    bool Hazmat,
    string EmployeeStatusCde);

public sealed record UpdateEmployeeRequest(
    string FirstNme,
    string? MiddleInitialNme,
    string LastNme,
    string? Suffix,
    bool Paramedic,
    bool Hazmat,
    string EmployeeStatusCde);

public sealed record EmployeeSummaryResponse(
    string Roic,
    int EmployeeNbr,
    string FirstNme,
    string LastNme,
    string EmployeeStatusCde);

public sealed record EmployeeDetailResponse(
    string Roic,
    int EmployeeNbr,
    string FirstNme,
    string? MiddleInitialNme,
    string LastNme,
    string? Suffix,
    bool Paramedic,
    bool Hazmat,
    string EmployeeStatusCde);

public sealed record EmployeeAssignmentRequest(
    DateOnly AssignmentStartDate,
    DateOnly? AssignmentEndDate,
    bool IsTemp,
    string Roic,
    int CompanyPositionId,
    int WorkPeriodId);

public sealed record EmployeeAssignmentResponse(
    int EmployeeAssignmentId,
    DateOnly AssignmentStartDate,
    DateOnly? AssignmentEndDate,
    bool IsTemp,
    string Roic,
    int CompanyPositionId,
    int WorkPeriodId);
