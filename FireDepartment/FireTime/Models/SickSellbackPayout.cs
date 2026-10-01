using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class SickSellbackPayout
{
    public int SickSellbackPayoutId { get; set; }

    public int SellbackPayoutYear { get; set; }

    public decimal SellbackPayoutHours { get; set; }

    public short PayTypeCde { get; set; }

    public DateTime? PayrollExportDateTime { get; set; }

    public bool DeletedInd { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string Roic { get; set; } = null!;

    public int SickSellbackSelectionId { get; set; }

    public virtual Employee RoicNavigation { get; set; } = null!;

    public virtual SickSellbackSelection SickSellbackSelection { get; set; } = null!;
}
