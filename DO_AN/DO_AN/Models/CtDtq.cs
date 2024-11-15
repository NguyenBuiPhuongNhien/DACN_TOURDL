using System;
using System.Collections.Generic;

namespace DO_AN.Models;

public partial class CtDtq
{
    public int Madtq { get; set; }

    public int Madl { get; set; }

    public string? Motachitiet { get; set; }

    public virtual Danhlamtc MadlNavigation { get; set; } = null!;

    public virtual Diemthamquan MadtqNavigation { get; set; } = null!;
}
