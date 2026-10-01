using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class WorkPeriod
{
    public int WorkPeriodId { get; set; }

    public string WeekDay { get; set; } = null!;

    public int WorkPeriodNbr { get; set; }

    public virtual ICollection<EmployeeAssignment> EmployeeAssignments { get; set; } = new List<EmployeeAssignment>();
}
