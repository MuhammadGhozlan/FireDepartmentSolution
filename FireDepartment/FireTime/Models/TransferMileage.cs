using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class TransferMileage
{
    public int TransferMileageId { get; set; }

    public DateTime ScheduledReportDate { get; set; }

    public DateTime TransferDateTime { get; set; }

    public bool DeletedInd { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string Roic { get; set; } = null!;

    public int StationDistanceId { get; set; }

    public virtual Employee RoicNavigation { get; set; } = null!;

    public virtual StationDistance StationDistance { get; set; } = null!;
}
