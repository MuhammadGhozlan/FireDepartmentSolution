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
    string EmployeeStatusCde,
    DateOnly? BirthDate,
    string? SocialSecurityNbr,
    string? MaritalStatusCde,
    DateTime CreatedDate,
    DateTime LastUpdateDate,
    string CreatedBy = "System",    
    string LastUpdateBy = "System"
    );

public sealed record UpdateEmployeeRequest(
    string FirstNme,
    string? MiddleInitialNme,
    string LastNme,
    string? Suffix,
    bool Paramedic,
    bool Hazmat,
    string EmployeeStatusCde,
    DateOnly? BirthDate,
    string? MaritalStatusCde,
    DateTime LastUpdateDate = default,
    string LastUpdateBy = "System"
    );

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
    string EmployeeStatusCde,
    DateOnly? BirthDate,
    string? MaritalStatusCde,
    DateTime CreatedDate,
    string CreatedBy,
    DateTime LastUpdateDate,
    string LastUpdateBy
    );

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
