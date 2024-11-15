using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DO_AN.Models;

public partial class Huongdan
{
    public int Manv { get; set; }

    public int Matour { get; set; }
    [Required(ErrorMessage = "Nhiệm vụ là bắt buộc")]
    public string? Nhiemvu { get; set; }

    public virtual Nhanvien ManvNavigation { get; set; } = null!;

    public virtual Tour MatourNavigation { get; set; } = null!;
}
