using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class LeaveTransaction
{
    public int LeaveTransactionId { get; set; }

    public decimal LeaveHours { get; set; }

    public DateOnly LeaveTransactionDate { get; set; }

    public bool DeletedInd { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string Roic { get; set; } = null!;

    public int LeaveDetailId { get; set; }

    public int LeaveTransactionTypeId { get; set; }

    public string? LeaveTransactionComments { get; set; }

    public virtual LeaveDetailCode LeaveDetail { get; set; } = null!;

    public virtual LeaveTransactionType LeaveTransactionType { get; set; } = null!;

    public virtual Employee RoicNavigation { get; set; } = null!;
}
