using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class Company
{
    public int CompanyId { get; set; }

    public string CompanyNme { get; set; } = null!;

    public bool IsActive { get; set; }

    public int CompanyMemberCount { get; set; }

    public string? MedicalClassification { get; set; }

    public string? CompanyComment { get; set; }

    public int? StationId { get; set; }

    public virtual ICollection<CompanyPosition> CompanyPositions { get; set; } = new List<CompanyPosition>();

    public virtual Station? Station { get; set; }

    public virtual ICollection<TransferRequest> TransferRequestCompanyFroms { get; set; } = new List<TransferRequest>();

    public virtual ICollection<TransferRequest> TransferRequestCompanyTos { get; set; } = new List<TransferRequest>();
}
