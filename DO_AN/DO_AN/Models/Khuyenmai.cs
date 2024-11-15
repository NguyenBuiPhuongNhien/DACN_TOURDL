using System;
using System.Collections.Generic;

namespace DO_AN.Models;

public partial class Khuyenmai
{
    public string Makm { get; set; } = null!;

    public double? Phantramkm { get; set; }

    public string? Tenkm { get; set; }

    public string? Dk { get; set; }

    public virtual ICollection<Phieudattour> Phieudattours { get; set; } = new List<Phieudattour>();
}
