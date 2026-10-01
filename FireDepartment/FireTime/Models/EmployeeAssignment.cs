using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class EmployeeAssignment
{
    public int EmployeeAssignmentId { get; set; }

    public DateOnly AssignmentStartDate { get; set; }

    public DateOnly? AssignmentEndDate { get; set; }

    public bool IsTemp { get; set; }

    public bool DeletedInd { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string Roic { get; set; } = null!;

    public int CompanyPositionId { get; set; }

    public int WorkPeriodId { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual CompanyPosition CompanyPosition { get; set; } = null!;

    public virtual Employee RoicNavigation { get; set; } = null!;

    public virtual WorkPeriod WorkPeriod { get; set; } = null!;
}
