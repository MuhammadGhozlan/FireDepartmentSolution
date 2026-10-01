using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class EmployeeStatusCode
{
    public string EmployeeStatusCde { get; set; } = null!;

    public string EmployeeStatusDesc { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
