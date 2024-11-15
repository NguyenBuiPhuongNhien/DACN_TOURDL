using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DO_AN.Models;

public partial class Diemthamquan
{
    public int Madtq { get; set; }

    public int Madd { get; set; }
    [Required(ErrorMessage = "Tên điểm tham quan là bắt buộc")]
    public string? Tendtq { get; set; }
    [Required(ErrorMessage = "Nhập đầy đủ thông tin về địa điểm")]
    public string? Thongtinct { get; set; }

    public virtual ICollection<CtDtq> CtDtqs { get; set; } = new List<CtDtq>();

    public virtual ICollection<Ctdd> Ctdds { get; set; } = new List<Ctdd>();

    public virtual Diemden MaddNavigation { get; set; } = null!;
}
