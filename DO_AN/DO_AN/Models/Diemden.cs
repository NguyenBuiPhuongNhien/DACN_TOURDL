using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DO_AN.Models;

public partial class Diemden
{
    public int Madd { get; set; }
    [Required(ErrorMessage = "Tên điểm đến là bắt buộc")]
    public string Tendd { get; set; } = null!;

    public virtual ICollection<Diemthamquan> Diemthamquans { get; set; } = new List<Diemthamquan>();

    public virtual ICollection<Khachsan> Khachsans { get; set; } = new List<Khachsan>();
}
