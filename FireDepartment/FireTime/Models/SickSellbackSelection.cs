using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class SickSellbackSelection
{
    public int SickSellbackSelectionId { get; set; }

    public int SellbackSelectionYear { get; set; }

    public DateTime SellbackSelectionDateTime { get; set; }

    public bool DeletedInd { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string Roic { get; set; } = null!;

    public int SickSellbackCodeId { get; set; }

    public int ShiftId { get; set; }

    public virtual Employee RoicNavigation { get; set; } = null!;

    public virtual Shift Shift { get; set; } = null!;

    public virtual SickSellbackCode SickSellbackCode { get; set; } = null!;

    public virtual ICollection<SickSellbackPayout> SickSellbackPayouts { get; set; } = new List<SickSellbackPayout>();
}
