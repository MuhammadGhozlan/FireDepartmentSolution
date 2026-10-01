using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class StationDistance
{
    public int StationDistanceId { get; set; }

    public decimal DistanceMiles { get; set; }

    public int FromStationId { get; set; }

    public int ToStationId { get; set; }

    public virtual Station FromStation { get; set; } = null!;

    public virtual Station ToStation { get; set; } = null!;

    public virtual ICollection<TransferMileage> TransferMileages { get; set; } = new List<TransferMileage>();
}
