using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class LeaveRequest
{
    public int LeaveRequestId { get; set; }

    public DateTime LeaveRequestSubmitDate { get; set; }

    public decimal LeaveRequestHours { get; set; }

    public DateTime LeaveStartDate { get; set; }

    public DateTime LeaveEndDate { get; set; }

    public string? ApproverComment { get; set; }

    public bool DeletedInd { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string RequesterRoic { get; set; } = null!;

    public int LeaveDetailId { get; set; }

    public string? ApproverRoic { get; set; }

    public int ApprovalStatusId { get; set; }

    public string? RequesterComment { get; set; }

    public virtual ApprovalStatus ApprovalStatus { get; set; } = null!;

    public virtual Employee? ApproverRoicNavigation { get; set; }

    public virtual LeaveDetailCode LeaveDetail { get; set; } = null!;

    public virtual Employee RequesterRoicNavigation { get; set; } = null!;
}
