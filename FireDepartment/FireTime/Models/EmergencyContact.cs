using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class EmergencyContact
{
    public int EmergencyContactId { get; set; }

    public string EmergencyContactFname { get; set; } = null!;

    public string EmergencyContactLname { get; set; } = null!;

    public bool DeletedInd { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public int ContactTypeId { get; set; }

    public int? AddressId { get; set; }

    public int? PhoneNumberId { get; set; }

    public string Roic { get; set; } = null!;

    public virtual Address? Address { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ContactType ContactType { get; set; } = null!;

    public virtual PhoneNumber? PhoneNumber { get; set; }

    public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
}
