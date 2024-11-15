using System;
using System.Collections.Generic;

namespace DO_AN.Models;

public partial class Diemkhoihanh
{
    public int Madkh { get; set; }

    public string Tendkh { get; set; } = null!;

    public string? Dc { get; set; }

    public string Sdt { get; set; } = null!;

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
