using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class EquityAttribute
{
    public int Aid { get; set; }

    public string? Aname { get; set; }

    public int? TabId { get; set; }

    public virtual EquityTab? Tab { get; set; }
}
