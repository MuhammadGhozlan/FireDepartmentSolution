using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class ContactType
{
    public int ContactTypeId { get; set; }

    public string ContactTypeDesc { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<EmailAddress> EmailAddresses { get; set; } = new List<EmailAddress>();

    public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; } = new List<EmergencyContact>();

    public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
}
