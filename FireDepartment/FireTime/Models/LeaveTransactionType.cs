using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class LeaveTransactionType
{
    public int LeaveTransactionTypeId { get; set; }

    public string LeaveTransactionDesc { get; set; } = null!;

    public virtual ICollection<LeaveDefaultHour> LeaveDefaultHours { get; set; } = new List<LeaveDefaultHour>();

    public virtual ICollection<LeaveTransaction> LeaveTransactions { get; set; } = new List<LeaveTransaction>();
}
