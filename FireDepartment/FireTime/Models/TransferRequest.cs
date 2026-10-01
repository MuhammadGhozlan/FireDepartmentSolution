using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class TransferRequest
{
    public int TransferRequestId { get; set; }

    public DateTime TransferRequestDate { get; set; }

    public bool HazMatRequested { get; set; }

    public bool HazMatPosition { get; set; }

    public bool ParamedicRequested { get; set; }

    public bool ParamedicPosition { get; set; }

    public string? TransferRequestComment { get; set; }

    public string? ApproverComment { get; set; }

    public bool DeletedInd { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string RequesterRoic { get; set; } = null!;

    public int RequesterJobTitleId { get; set; }

    public int ShiftFromId { get; set; }

    public int ShiftToId { get; set; }

    public int StationFromId { get; set; }

    public int StationToId { get; set; }

    public int CompanyFromId { get; set; }

    public int CompanyToId { get; set; }

    public int CompanyPositionFromId { get; set; }

    public int CompanyPositionToId { get; set; }

    public string? ApproverRoic { get; set; }

    public int ApprovalStatusId { get; set; }

    public bool IsTemp { get; set; }

    public virtual ApprovalStatus ApprovalStatus { get; set; } = null!;

    public virtual Employee? ApproverRoicNavigation { get; set; }

    public virtual Company CompanyFrom { get; set; } = null!;

    public virtual CompanyPosition CompanyPositionFrom { get; set; } = null!;

    public virtual CompanyPosition CompanyPositionTo { get; set; } = null!;

    public virtual Company CompanyTo { get; set; } = null!;

    public virtual JobTitle RequesterJobTitle { get; set; } = null!;

    public virtual Employee RequesterRoicNavigation { get; set; } = null!;

    public virtual Shift ShiftFrom { get; set; } = null!;

    public virtual Shift ShiftTo { get; set; } = null!;

    public virtual Station StationFrom { get; set; } = null!;

    public virtual Station StationTo { get; set; } = null!;
}
