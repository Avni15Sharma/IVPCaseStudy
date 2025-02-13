using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class BondTab
{
    public int TabId { get; set; }

    public string? TabName { get; set; }

    public virtual ICollection<BondAttribute> BondAttributes { get; set; } = new List<BondAttribute>();
}
