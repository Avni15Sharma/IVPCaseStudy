using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class EquityTab
{
    public int TabId { get; set; }

    public string? TabName { get; set; }

    public virtual ICollection<EquityAttribute> EquityAttributes { get; set; } = new List<EquityAttribute>();
}
