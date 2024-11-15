using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace DO_AN.Models;

public partial class Loaitour
{
    public int Maloai { get; set; }
    [Required(ErrorMessage = "Tên  phương tiện không được để trống")]
    public string Tenloai { get; set; } = null!;

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
