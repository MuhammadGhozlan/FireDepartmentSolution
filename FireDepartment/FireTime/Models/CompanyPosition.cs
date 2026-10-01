using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class CompanyPosition
{
    public int CompanyPositionId { get; set; }

    public byte TruckPosition { get; set; }

    public int CompanyId { get; set; }

    public int JobTitleId { get; set; }

    public int ShiftId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<EmployeeAssignment> EmployeeAssignments { get; set; } = new List<EmployeeAssignment>();

    public virtual JobTitle JobTitle { get; set; } = null!;

    public virtual Shift Shift { get; set; } = null!;

    public virtual ICollection<TransferRequest> TransferRequestCompanyPositionFroms { get; set; } = new List<TransferRequest>();

    public virtual ICollection<TransferRequest> TransferRequestCompanyPositionTos { get; set; } = new List<TransferRequest>();
}
