using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class Shift
{
    public int ShiftId { get; set; }

    public string ShiftCode { get; set; } = null!;

    public string? ShiftNme { get; set; }

    public byte? SortOrder { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<CompanyPosition> CompanyPositions { get; set; } = new List<CompanyPosition>();

    public virtual ICollection<LeaveDefaultHour> LeaveDefaultHours { get; set; } = new List<LeaveDefaultHour>();

    public virtual ICollection<SickSellbackCode> SickSellbackCodes { get; set; } = new List<SickSellbackCode>();

    public virtual ICollection<SickSellbackSelection> SickSellbackSelections { get; set; } = new List<SickSellbackSelection>();

    public virtual ICollection<TransferRequest> TransferRequestShiftFroms { get; set; } = new List<TransferRequest>();

    public virtual ICollection<TransferRequest> TransferRequestShiftTos { get; set; } = new List<TransferRequest>();
}
