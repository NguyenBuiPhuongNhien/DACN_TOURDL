using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DO_AN.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public string? Address { get; set; }
        public DateOnly? Ngaysinh { get; set; }
        public string? Gioitinh { get; set; }
        public string? Sdt { get; set; }
    }
}
