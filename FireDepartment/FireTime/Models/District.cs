using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class District
{
    public int DistrictId { get; set; }

    public string DistrictNme { get; set; } = null!;

    public int? StationCount { get; set; }

    public bool IsActive { get; set; }

    public string? DistrictComment { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();

    public virtual ICollection<Station> Stations { get; set; } = new List<Station>();
}
