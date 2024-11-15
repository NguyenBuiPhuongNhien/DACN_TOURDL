using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DO_AN.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IKhachhangRepository _khachhangRepository;
        private readonly INhanvienRepository _nhanvienRepository;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager, IKhachhangRepository khachhangRepository, INhanvienRepository nhanvienRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _khachhangRepository = khachhangRepository;
            _nhanvienRepository = nhanvienRepository;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            //[Phone]
            //[Display(Name = "Phone number")]
            //public string PhoneNumber { get; set; }

            public string hoten { get; set; }

            public string Sdt { get; set; }

            public string Dc { get; set; }

            [DataType(DataType.Date)]
            public DateOnly? Ngaysinh { get; set; }

            [RegularExpression("Nam|Nữ", ErrorMessage = "Giới tính chỉ được chọn Nam hoặc Nữ")]
            public string? Gioitinh { get; set; }

        }

        private async Task LoadAsync(User user)
        {

            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var khachhang = await _khachhangRepository.GetBySdtAsync(user.Sdt);
            var nhanvien = await _nhanvienRepository.GetBySdtAsync(user.Sdt);

            Username = userName;
            if (User.IsInRole("Admin") || User.IsInRole("Nhân viên"))
            {
                if (nhanvien == null)
                {
                    StatusMessage = "Không tìm thấy nhân viên.";
                }
                else
                {
                    Input = new InputModel
                    {
                        hoten = user.FullName,
                        Sdt = user.Sdt, // Thay đổi tên thuộc tính phù hợp với tên thuộc tính trong User model
                        Dc = nhanvien.Dc,
                        Ngaysinh = nhanvien.Ngaysinh,
                        Gioitinh = nhanvien.Gioitinh
                    };
                }

            }
            else
            {
                if (khachhang == null)
                {
                    StatusMessage = "Không tìm thấy khách hàng.";
                }
                else
                {
                    Input = new InputModel
                    {
                        hoten = khachhang.Tenkh,
                        Sdt = khachhang.Sdt, // Thay đổi tên thuộc tính phù hợp với tên thuộc tính trong User model
                        Dc = khachhang.Dc,
                    };
                }
            }


        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Không thể tải người dùng có ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Không thể tải người dùng có ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            var khachhang = await _khachhangRepository.GetBySdtAsync(user.Sdt);
            var nhanvien = await _nhanvienRepository.GetBySdtAsync(user.Sdt);

            if (User.IsInRole("Admin") || User.IsInRole("Nhân viên"))
            {
                if (nhanvien == null)
                {
                    StatusMessage = "Không tìm thấy nhân viên.";
                }
                else
                {
                    var ngaysinh = Input.Ngaysinh.Value.Year;
                    var namhientai = DateTime.Now.Year;
                    user.FullName = Input.hoten;
                    user.Sdt = Input.Sdt;
                    user.Address = Input.Dc;
                    if (namhientai - ngaysinh < 18)
                    {
                        StatusMessage = "Nhân viên phải đủ 18 tuổi";
                        await LoadAsync(user);
                        return Page();
                    }
                    user.Ngaysinh = Input.Ngaysinh;
                    user.Gioitinh = Input.Gioitinh;
                    nhanvien.Sdt = Input.Sdt;
                    nhanvien.Dc = Input.Dc;
                    nhanvien.Ngaysinh = Input.Ngaysinh;
                    nhanvien.Gioitinh = Input.Gioitinh;

                    await _nhanvienRepository.UpdateAsync(nhanvien);
                }
            }
            else
            {
                if (khachhang == null)
                {
                    StatusMessage = "Không tìm thấy khách hàng.";
                }
                else
                {
                    user.FullName = Input.hoten;
                    user.Sdt = Input.Sdt;
                    user.Address = Input.Dc;
                    khachhang.Sdt = Input.Sdt;
                    khachhang.Dc = Input.Dc;
                    await _khachhangRepository.UpdateAsync(khachhang);
                }
            }
            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //if (Input.PhoneNumber != phoneNumber)
            //{
            //    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            //    if (!setPhoneResult.Succeeded)
            //    {
            //        StatusMessage = "Lỗi không mong muốn khi cố gắng đặt số điện thoại.";
            //        return RedirectToPage();
            //    }
            //}

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Hồ sơ của bạn đã được cập nhật";
            return RedirectToPage();
        }
    }
}
