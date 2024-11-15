using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DO_AN.Models;

public partial class Danhgia
{
    public int Makh { get; set; }

    public int Matour { get; set; }

    public string? Noidungdanhgia { get; set; }
    
    [Range(1, 5, ErrorMessage = "Số sao phải từ 1 đến 5")]
    public int? Sosao { get; set; }

    public DateOnly? Thoigiandanhgia { get; set; }

    public virtual Khachhang MakhNavigation { get; set; } = null!;

    public virtual Tour MatourNavigation { get; set; } = null!;
}
