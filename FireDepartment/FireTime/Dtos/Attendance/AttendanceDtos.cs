namespace FireTime.Dtos.Attendance;

public sealed record AttendanceRequest(
    DateOnly? AttendanceDate,
    string? Roic,
    int? EmployeeAssignmentId,
    int? AttendanceStatusId,
    string? AttendanceComments,
    bool? DeletedInd,
    DateOnly? StartDate,
    DateOnly? EndDate
    );

public sealed record AttendanceResponse(
    int AttendanceId,
    DateOnly AttendanceDate,
    string Roic,
    int EmployeeAssignmentId,
    int AttendanceStatusId,
    string? AttendanceComments,
    bool? DeletedInd);
