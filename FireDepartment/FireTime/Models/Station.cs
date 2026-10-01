using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class Station
{
    public int StationId { get; set; }

    public string StationNbr { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? StationComment { get; set; }

    public int DistrictId { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual District District { get; set; } = null!;

    public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();

    public virtual ICollection<StationDistance> StationDistanceFromStations { get; set; } = new List<StationDistance>();

    public virtual ICollection<StationDistance> StationDistanceToStations { get; set; } = new List<StationDistance>();

    public virtual ICollection<TransferRequest> TransferRequestStationFroms { get; set; } = new List<TransferRequest>();

    public virtual ICollection<TransferRequest> TransferRequestStationTos { get; set; } = new List<TransferRequest>();
}
