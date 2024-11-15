using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace DO_AN.Models;

public partial class Lichtrinh
{
    public int Malt { get; set; }

    [Required(ErrorMessage = "Tên lịch trình không được để trống")]
    public string Tenlt { get; set; }

    public virtual ICollection<Ctdd> Ctdds { get; set; } = new List<Ctdd>();

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
