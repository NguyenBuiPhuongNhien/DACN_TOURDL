using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DO_AN.Models;

public partial class Phieudattour
{
    public int Mapdt { get; set; }

    public int Matour { get; set; }

    public int Makh { get; set; }
    [NotMapped]
    public string Payment { get; set; }
    public bool DaThanhToan { get; set; } = false;
    public string? Makm { get; set; } = null!;
    [Required(ErrorMessage ="Số người không được nhỏ hơn hoặc bằng 0")]
    [Range(1, 30, ErrorMessage = "Số người đăng ký không được bằng hoặc dưới 1")]
    public int? Song { get; set; }

    [DataType(DataType.Date)]
    public DateOnly? Ngaydat { get; set; }

    public float? Tongtien { get; set; }

    public string? Dvt { get; set; }

    public virtual Khachhang MakhNavigation { get; set; } = null!;

    public virtual Khuyenmai MakmNavigation { get; set; } = null!;

    public virtual Tour MatourNavigation { get; set; } = null!;
}
