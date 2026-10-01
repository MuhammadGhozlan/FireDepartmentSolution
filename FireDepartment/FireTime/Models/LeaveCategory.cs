using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class LeaveCategory
{
    public int LeaveCategoryId { get; set; }

    public string LeaveCategoryCode { get; set; } = null!;

    public string LeaveCategoryDesc { get; set; } = null!;

    public virtual ICollection<LeaveDefaultHour> LeaveDefaultHours { get; set; } = new List<LeaveDefaultHour>();

    public virtual ICollection<LeaveDetailCode> LeaveDetailCodes { get; set; } = new List<LeaveDetailCode>();
}
