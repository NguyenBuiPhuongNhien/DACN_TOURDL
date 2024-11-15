using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DO_AN.Models;

public partial class Phuongtiendc
{
    public int Mapt { get; set; }
    [Required (ErrorMessage ="Tên  phương tiện không được để trống")]
    public string? Tenpt { get; set; }

    public virtual ICollection<Ctpt> Ctpts { get; set; } = new List<Ctpt>();
}
