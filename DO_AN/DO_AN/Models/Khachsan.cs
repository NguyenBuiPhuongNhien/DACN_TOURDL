using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DO_AN.Models;

public partial class Khachsan
{
    public int Maks { get; set; }

    public int Madd { get; set; }
    [Required(ErrorMessage = "Tên khách sạn không được để trống")]
    public string? Tenks { get; set; }
    [Required(ErrorMessage = "Địa chỉ không được để trống")]
    public string? Dc { get; set; }

    public virtual ICollection<Ctdd> Ctdds { get; set; } = new List<Ctdd>();

    public virtual Diemden MaddNavigation { get; set; } = null!;
}
