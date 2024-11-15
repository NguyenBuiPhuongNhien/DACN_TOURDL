using System.Threading.Tasks;
using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DO_AN.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<PersonalDataModel> _logger;
        private readonly INhanvienRepository _nhanvienRepository;
        private readonly IKhachhangRepository _khachhangRepository;

        [TempData]
        public string StatusMessage { get; set; }

        public Nhanvien Nhanvien { get; set; }
        public Khachhang Khachhang { get; set; }

        public PersonalDataModel(
            UserManager<User> userManager,
            ILogger<PersonalDataModel> logger,
            IKhachhangRepository khachhangRepository,
            INhanvienRepository nhanvienRepository)
        {
            _userManager = userManager;
            _logger = logger;
            _khachhangRepository = khachhangRepository;
            _nhanvienRepository = nhanvienRepository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Unable to load user.");
            }

            if (User.IsInRole("Admin") || User.IsInRole("Nhân viên"))
            {
                Nhanvien = await _nhanvienRepository.GetBySdtAsync(user.Sdt);
                if (Nhanvien == null)
                {
                    StatusMessage = "Không tìm thấy nhân viên.";
                    return Page();
                }
            }
            else
            {
                Khachhang = await _khachhangRepository.GetBySdtAsync(user.Sdt);
                if (Khachhang == null)
                {
                    StatusMessage = "Không tìm thấy khách hàng.";
                    return Page();
                }
            }
            return Page();
        }
    }
}
