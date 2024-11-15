using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DO_AN.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien)]
    public class KhachhangController : Controller
    {
        private readonly IKhachhangRepository _khachhangRepository;
        private readonly IPhieudattourRepository _phieudattourRepository;
        private readonly ILoaitourRepository _loaitourRepository;
        private readonly IDiemdenRepository _diemdenRepository;
        public KhachhangController(IKhachhangRepository khachhangRepository, IPhieudattourRepository phieudattourRepository, ILoaitourRepository loaitourRepository, IDiemdenRepository diemdenRepository)
        {
            _khachhangRepository = khachhangRepository;
            _phieudattourRepository = phieudattourRepository;
            _loaitourRepository = loaitourRepository;
            _diemdenRepository = diemdenRepository;
        }

        public async Task<IActionResult> Index()
        {
            var khachhangs = await _khachhangRepository.GetAllAsync();
            foreach (var item in khachhangs)
            {
                await _phieudattourRepository.UpdateSLTourDaDat(item.Makh);
            }
            return View(khachhangs);
        }

        public async Task<IActionResult> AddAsync()
        {
            return View();
        }
        public async Task<IActionResult> Display(int makh)
        {
            var khachhang = await _khachhangRepository.GetByIdAsync(makh);
            if (khachhang == null)
            {
                return NotFound();
            }
            return View(khachhang);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Khachhang khachhang)
        {
            if (ModelState.IsValid)
            {
                var checksdt = await _khachhangRepository.GetAllAsync();
                if (checksdt != null)
                {
                    foreach(var item in checksdt)
                    {
                        if(khachhang.Sdt ==  item.Sdt)
                        {
                            ModelState.AddModelError("Sdt", "Số điện thoại đã có người đăng ký vui lòng nhập số khác");
                            return View(khachhang);
                        }    
                    }
                }
                khachhang.Sotourdadat = 0;
                await _khachhangRepository.AddAsync(khachhang);
                return RedirectToAction(nameof(Index));
            }
            return View(khachhang);
        }

        public async Task<IActionResult> Update(int makh)
        {
            var khachhang = await _khachhangRepository.GetByIdAsync(makh);
            if (khachhang == null)
            {
                return NotFound();
            }
            return View(khachhang);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int makh, Khachhang khachhang)
        {
            if (makh != khachhang.Makh)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingKhachhang = await _khachhangRepository.GetByIdAsync(makh);
                existingKhachhang.Tenkh = khachhang.Tenkh;
                existingKhachhang.Sdt = khachhang.Sdt;
                existingKhachhang.Dc = khachhang.Dc;
                existingKhachhang.Sotourdadat = khachhang.Sotourdadat;
                await _khachhangRepository.UpdateAsync(existingKhachhang);
                return RedirectToAction(nameof(Index));
            }
            return View(khachhang);
        }

        public async Task<IActionResult> Delete(int makh)
        {
            var khachhang = await _khachhangRepository.GetByIdAsync(makh);
            if (khachhang == null)
            {
                return NotFound();
            }
            return View(khachhang);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int makh)
        {
            await _khachhangRepository.DeleteAsync(makh);
            return RedirectToAction(nameof(Index));
        }
    }
}
