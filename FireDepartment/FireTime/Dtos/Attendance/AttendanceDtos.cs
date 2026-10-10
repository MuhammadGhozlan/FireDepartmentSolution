namespace FireTime.Dtos.Attendance;

public sealed record AttendanceRequest(
    DateOnly AttendanceDate,
    string Roic,    
    int AttendanceStatusId,
    string? AttendanceComments 
    );
public sealed record AttendanceFilterRequest(
    DateOnly? AttendanceDate,
    string? Roic,
    int? EmployeeAssignmentId,
    int? AttendanceStatusId,
    string? AttendanceComments,      
    DateOnly? StartDate,
    DateOnly? EndDate
    );
public sealed record UpdateAttendanceRequest(     
    DateOnly? AttendanceDate,
    string? Roic,     
    int? AttendanceStatusId,
    string? AttendanceComments
    );

public sealed record AttendanceResponse(
    int AttendanceId,
    DateOnly AttendanceDate,
    string Roic,
    int EmployeeAssignmentId,
    int AttendanceStatusId,
    string? AttendanceComments,
    bool? DeletedInd);
