using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class JobTitle
{
    public int JobTitleId { get; set; }

    public string JobTitleDesc { get; set; } = null!;

    public string JobTitleShortDesc { get; set; } = null!;

    public bool IsActive { get; set; }

    public string SalaryRangeGradeCde { get; set; } = null!;

    public virtual ICollection<CompanyPosition> CompanyPositions { get; set; } = new List<CompanyPosition>();

    public virtual ICollection<TransferRequest> TransferRequests { get; set; } = new List<TransferRequest>();
}
