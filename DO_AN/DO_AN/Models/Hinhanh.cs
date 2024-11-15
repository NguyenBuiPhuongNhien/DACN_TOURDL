using System;
using System.Collections.Generic;

namespace DO_AN.Models;

public partial class Hinhanh
{
    public int Mah { get; set; }

    public int Madl { get; set; }

    public string Url { get; set; } = null!;

    public virtual Danhlamtc MadlNavigation { get; set; } = null!;
}
