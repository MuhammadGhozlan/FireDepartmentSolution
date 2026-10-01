using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class LeaveDefaultHour
{
    public int LeaveDefaultHoursId { get; set; }

    public decimal DefaultHours { get; set; }

    public int LeaveCategoryId { get; set; }

    public int LeaveTransactionTypeId { get; set; }

    public int ShiftId { get; set; }

    public int LeaveDetailId { get; set; }

    public virtual LeaveCategory LeaveCategory { get; set; } = null!;

    public virtual LeaveDetailCode LeaveDetail { get; set; } = null!;

    public virtual LeaveTransactionType LeaveTransactionType { get; set; } = null!;

    public virtual Shift Shift { get; set; } = null!;
}
