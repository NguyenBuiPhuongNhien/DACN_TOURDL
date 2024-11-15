using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DO_AN.Models;

public partial class Tour
{
    public int Matour { get; set; }

    public int Malt { get; set; }

    public int Maloai { get; set; }

    public int Madkh { get; set; }
    [Required(ErrorMessage = "Tên tuor là bắt buộc")]
    public string Tentour { get; set; } = null!;

    [Required(ErrorMessage = "Nhập ngày khởi hành tour")]
    [DataType(DataType.Date)]
    public DateOnly Ngaykh { get; set; }
    [Required(ErrorMessage = "Nhập ngày kết thúc tour")]
    [DataType(DataType.Date)]
    public DateOnly? Ngaykt { get; set; }

    public int? Songay { get; set; }

    public int? Sodem { get; set; }
    [Required(ErrorMessage = "Nhập số người tối đa trong tour")]
    [Range(10, 30, ErrorMessage = "Số người tối đa ít nhất là 10 người nhiều nhất 30 người")]
    public int? Soluongtoida { get; set; }

    public int? Sochodadat { get; set; }
    [Required(ErrorMessage = "Giá tour là bắt buộc.")]
    [Range(100000, 10000000, ErrorMessage = "Giá tour phải lớn hơn 100.000 và nhỏ hơn 10.000.000.")]
    public float? Gia { get; set; }

    [RegularExpression("VND|Dola", ErrorMessage = "Đơn vị tính chỉ được nhập VND hoặc Dola")]
    public string? Dvt { get; set; }

    public string? Anhdaidien { get; set; }

    public virtual ICollection<Ctpt> Ctpts { get; set; } = new List<Ctpt>();

    public virtual ICollection<Danhgia> Danhgia { get; set; } = new List<Danhgia>();

    public virtual ICollection<Huongdan> Huongdans { get; set; } = new List<Huongdan>();



    [Required]
    public virtual Diemkhoihanh MadkhNavigation { get; set; } = null!;
    [Required]
    public virtual Loaitour MaloaiNavigation { get; set; } = null!;
    [Required]
    public virtual Lichtrinh MaltNavigation { get; set; } = null!;

    public virtual ICollection<Phieudattour> Phieudattours { get; set; } = new List<Phieudattour>();
}
