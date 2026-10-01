using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class Employee
{
    public string Roic { get; set; } = null!;

    public int EmployeeNbr { get; set; }

    public string FirstNme { get; set; } = null!;

    public string? MiddleInitialNme { get; set; }

    public string LastNme { get; set; } = null!;

    public string? Suffix { get; set; }

    public string? SocialSecurityNbr { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? MaritalStatusCde { get; set; }

    public bool Paramedic { get; set; }

    public bool Hazmat { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string EmployeeStatusCde { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<EmailAddress> EmailAddresses { get; set; } = new List<EmailAddress>();

    public virtual ICollection<EmployeeAssignment> EmployeeAssignments { get; set; } = new List<EmployeeAssignment>();

    public virtual EmployeeStatusCode EmployeeStatusCdeNavigation { get; set; } = null!;

    public virtual ICollection<LeaveRequest> LeaveRequestApproverRoicNavigations { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<LeaveRequest> LeaveRequestRequesterRoicNavigations { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<LeaveTransaction> LeaveTransactions { get; set; } = new List<LeaveTransaction>();

    public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();

    public virtual ICollection<SickSellbackPayout> SickSellbackPayouts { get; set; } = new List<SickSellbackPayout>();

    public virtual ICollection<SickSellbackSelection> SickSellbackSelections { get; set; } = new List<SickSellbackSelection>();

    public virtual ICollection<TransferMileage> TransferMileages { get; set; } = new List<TransferMileage>();

    public virtual ICollection<TransferRequest> TransferRequestApproverRoicNavigations { get; set; } = new List<TransferRequest>();

    public virtual ICollection<TransferRequest> TransferRequestRequesterRoicNavigations { get; set; } = new List<TransferRequest>();
}
