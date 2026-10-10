using System.ComponentModel.DataAnnotations;

namespace FireTime.Dtos.Attendance;

public sealed record AttendanceRequest(
    DateOnly AttendanceDate,
    string Roic,
    [Range(1, int.MaxValue, ErrorMessage = "A valid AttendanceStatusId is required.")]
    int AttendanceStatusId,
    string? AttendanceComments,
    int? EmployeeAssignmentId = null
    );
public sealed record AttendanceFilterRequest(
    DateOnly? AttendanceDate,
    string? Roic,
    int? EmployeeAssignmentId,
    [Range(1, int.MaxValue, ErrorMessage = "A valid AttendanceStatusId is required.")]
    int? AttendanceStatusId,
    string? AttendanceComments,      
    DateOnly? StartDate,
    DateOnly? EndDate
    );
public sealed record UpdateAttendanceRequest(     
    DateOnly? AttendanceDate,
    string? Roic,
    [Range(1, int.MaxValue, ErrorMessage = "A valid AttendanceStatusId is required.")]
    int? AttendanceStatusId,
    string? AttendanceComments,
    int? EmployeeAssignmentId = null
    );

public sealed record AttendanceResponse(
    int AttendanceId,
    DateOnly AttendanceDate,
    string Roic,
    int EmployeeAssignmentId,
    int AttendanceStatusId,
    string? AttendanceComments,
    bool? DeletedInd);

public sealed record AttendanceAssignmentResponse(
    int EmployeeAssignmentId,
    string Roic,
    DateOnly AssignmentStartDate,
    DateOnly? AssignmentEndDate,
    bool IsTemp,
    int CompanyPositionId,
    string CompanyNme,
    string ShiftCode,
    int WorkPeriodNbr);

public sealed record AttendanceRosterResponse(
    DateOnly AttendanceDate,
    int EmployeeAssignmentId,
    string Roic,
    int EmployeeNbr,
    string FirstNme,
    string LastNme,
    string CompanyNme,
    string ShiftCode,
    int WorkPeriodNbr,
    List<AttendanceResponse> Attendances);
