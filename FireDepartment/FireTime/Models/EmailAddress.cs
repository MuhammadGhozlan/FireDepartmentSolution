using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class EmailAddress
{
    public int EmailAddressId { get; set; }

    public string EmailAddressTxt { get; set; } = null!;

    public bool DeletedInd { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string? Roic { get; set; }

    public int ContactTypeId { get; set; }

    public virtual ContactType ContactType { get; set; } = null!;

    public virtual Employee? RoicNavigation { get; set; }
}
