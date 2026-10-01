using System;
using System.Collections.Generic;

namespace FireTime.Models;

public partial class AuditHistory
{
    public int AuditHistoryId { get; set; }

    public string TableNme { get; set; } = null!;

    public string FieldNme { get; set; } = null!;

    public string? OldValue { get; set; }

    public string NewValue { get; set; } = null!;

    public DateTime AuditHistoryDate { get; set; }

    public string AuditHistoryUpdateBy { get; set; } = null!;
}
