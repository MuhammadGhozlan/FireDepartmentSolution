using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class AttendanceStatus
{
    public int AttendanceStatusId { get; set; }

    public string AttendanceStatusCde { get; set; } = null!;

    public string AttendanceStatusDesc { get; set; } = null!;

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
}
