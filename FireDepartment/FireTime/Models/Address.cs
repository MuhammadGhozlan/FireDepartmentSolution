using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public string StreetAddressTxt { get; set; } = null!;

    public string CityNme { get; set; } = null!;

    public string StateNme { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public bool DeletedInd { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string? Roic { get; set; }

    public int? DistrictId { get; set; }

    public int? StationId { get; set; }

    public int? EmergencyContactId { get; set; }

    public int ContactTypeId { get; set; }

    public virtual ContactType ContactType { get; set; } = null!;

    public virtual District? District { get; set; }

    public virtual EmergencyContact? EmergencyContact { get; set; }

    public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; } = new List<EmergencyContact>();

    public virtual Employee? RoicNavigation { get; set; }

    public virtual Station? Station { get; set; }
}
