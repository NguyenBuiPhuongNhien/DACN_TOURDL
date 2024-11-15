using System;
using System.Collections.Generic;

namespace DO_AN.Models;

public partial class Ctpt
{
    public int Matour { get; set; }

    public int Mapt { get; set; }

    public string? Bienso { get; set; }

    public virtual Phuongtiendc MaptNavigation { get; set; } = null!;

    public virtual Tour MatourNavigation { get; set; } = null!;
}
