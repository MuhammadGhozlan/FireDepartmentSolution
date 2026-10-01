using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class LeaveDetailCode
{
    public int LeaveDetailId { get; set; }

    public string LeaveDetailCode1 { get; set; } = null!;

    public string LeaveDetailDesc { get; set; } = null!;

    public int LeaveCategoryId { get; set; }

    public virtual LeaveCategory LeaveCategory { get; set; } = null!;

    public virtual ICollection<LeaveDefaultHour> LeaveDefaultHours { get; set; } = new List<LeaveDefaultHour>();

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<LeaveTransaction> LeaveTransactions { get; set; } = new List<LeaveTransaction>();
}
