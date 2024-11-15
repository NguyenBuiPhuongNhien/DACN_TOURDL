using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DO_AN.Models;

public partial class Nhanvien
{
    public int Manv { get; set; }
    [Required(ErrorMessage = "Tên là bắt buộc")]
    public string? Tennv { get; set; }

    //[Required(ErrorMessage = "Nhân viên phải đủ 18 tuổi trở lên mới có thể làm việc!!")]
    [DataType(DataType.Date)]
    public DateOnly? Ngaysinh { get; set; }

    //[RegularExpression("Nam|Nữ", ErrorMessage = "Giới tính chỉ được chọn Nam hoặc Nữ")]
    public string? Gioitinh { get; set; }
    [RegularExpression("^[0-9]*$", ErrorMessage = "Số điện thoại chỉ được chứa chữ số")]
    public string? Sdt { get; set; }

    public string? Dc { get; set; }

    public virtual ICollection<Huongdan> Huongdans { get; set; } = new List<Huongdan>();
}
