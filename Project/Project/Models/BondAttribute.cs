using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class BondAttribute
{
    public int Aid { get; set; }

    public string? Aname { get; set; }

    public int? TabId { get; set; }

    public virtual BondTab? Tab { get; set; }
}
