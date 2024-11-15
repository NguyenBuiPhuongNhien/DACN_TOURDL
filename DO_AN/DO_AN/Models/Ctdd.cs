using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DO_AN.Models;

public partial class Ctdd
{
    public int Malt { get; set; }

    public int Maks { get; set; }

    public int Madtq { get; set; }
    [Required(ErrorMessage = "Nhập thứ tự lộ trình của tour")]
    [Range(1,30 , ErrorMessage = "Thứ tự phải lớn hơn 0")]
    public int? Thutu { get; set; }

    public DateOnly? Ngay { get; set; }

    public virtual Diemthamquan MadtqNavigation { get; set; } = null!;

    public virtual Khachsan MaksNavigation { get; set; } = null!;

    public virtual Lichtrinh MaltNavigation { get; set; } = null!;
}
