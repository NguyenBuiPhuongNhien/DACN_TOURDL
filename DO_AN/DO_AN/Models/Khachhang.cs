using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DO_AN.Models;

public partial class Khachhang
{
    public int Makh { get; set; }
    [Required(ErrorMessage = "Tên không được để trống")]
    public string? Tenkh { get; set; }
    [RegularExpression("^[0-9]*$", ErrorMessage = "Số điện thoại chỉ được chứa chữ số")]
    public string? Sdt { get; set; }
   
    public string? Dc { get; set; }

    public int? Sotourdadat { get; set; }

    public virtual ICollection<Danhgia> Danhgia { get; set; } = new List<Danhgia>();

    public virtual ICollection<Phieudattour> Phieudattours { get; set; } = new List<Phieudattour>();
}
