using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class SickSellbackCode
{
    public int SickSellbackCodeId { get; set; }

    public string SickSellbackCode1 { get; set; } = null!;

    public string SickSellbackDesc { get; set; } = null!;

    public string? SickSellbackCodeComments { get; set; }

    public int ShiftId { get; set; }

    public virtual Shift Shift { get; set; } = null!;

    public virtual ICollection<SickSellbackSelection> SickSellbackSelections { get; set; } = new List<SickSellbackSelection>();
}
