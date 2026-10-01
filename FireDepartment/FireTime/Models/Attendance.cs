using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public DateOnly AttendanceDate { get; set; }

    public bool DeletedInd { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string Roic { get; set; } = null!;

    public int EmployeeAssignmentId { get; set; }

    public int AttendanceStatusId { get; set; }

    public string? AttendanceComments { get; set; }

    public virtual AttendanceStatus AttendanceStatus { get; set; } = null!;

    public virtual EmployeeAssignment EmployeeAssignment { get; set; } = null!;

    public virtual Employee RoicNavigation { get; set; } = null!;
}
