using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DO_AN.Models;

public partial class Danhlamtc
{
    public int Madl { get; set; }
    [Required(ErrorMessage = "Tên danh lam thắng cảnh là bắt buộc")]
    public string? Tendl { get; set; }

    public virtual ICollection<CtDtq> CtDtqs { get; set; } = new List<CtDtq>();

    public virtual ICollection<Hinhanh> Hinhanhs { get; set; } = new List<Hinhanh>();
}
