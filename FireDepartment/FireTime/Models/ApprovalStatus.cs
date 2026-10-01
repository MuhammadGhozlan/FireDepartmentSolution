using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class ApprovalStatus
{
    public int ApprovalStatusId { get; set; }

    public string ApprovalStatusCde { get; set; } = null!;

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<TransferRequest> TransferRequests { get; set; } = new List<TransferRequest>();
}
