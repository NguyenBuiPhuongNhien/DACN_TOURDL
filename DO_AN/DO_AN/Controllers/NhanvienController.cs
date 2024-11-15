using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DO_AN.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class NhanvienController : Controller
    {
        private readonly INhanvienRepository _nhanvienRepository;
        private readonly ILoaitourRepository loaitourRepository;
        private readonly IDiemdenRepository diemdenRepository;

        public NhanvienController(INhanvienRepository nhanvienRepository, ILoaitourRepository loaitourRepository, IDiemdenRepository diemdenRepository)
        {
            _nhanvienRepository = nhanvienRepository;
            this.loaitourRepository = loaitourRepository;
            this.diemdenRepository = diemdenRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            var nhanviens = await _nhanvienRepository.GetAllAsync();
            return View(nhanviens);
        }
        public async Task<IActionResult> Display(int manv)
        {
            var nhanvien = await _nhanvienRepository.GetByIdAsync(manv);
            if (nhanvien == null)
            {
                return NotFound();
            }
            return View(nhanvien);
        }
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Nhanvien nhanvien)
        {
            if (ModelState.IsValid)
            {
                var ngaysinh = nhanvien.Ngaysinh.Value.Year;
                var namhientai = DateTime.Now.Year;
                if(namhientai - ngaysinh < 18)
                {
                    ModelState.AddModelError("Ngaysinh", "Nhân viên phải đủ 18 tuổi");
                    return View(nhanvien);
                }
                await _nhanvienRepository.AddAsync(nhanvien);
                return RedirectToAction(nameof(Index));
            }
            return View(nhanvien);
        }

        public async Task<IActionResult> Update(int manv)
        {
            var nhanvien = await _nhanvienRepository.GetByIdAsync(manv);
            if (nhanvien == null)
            {
                return NotFound();
            }
            return View(nhanvien);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int manv, Nhanvien nhanvien)
        {
            if (manv != nhanvien.Manv)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var ngaysinh = nhanvien.Ngaysinh.Value.Year;
                var namhientai = DateTime.Now.Year;
                if (namhientai - ngaysinh < 18)
                {
                    ModelState.AddModelError("Ngaysinh", "Nhân viên phải đủ 18 tuổi");
                    return View(nhanvien);
                }
                var existingNhanvien = await _nhanvienRepository.GetByIdAsync(manv);
                existingNhanvien.Tennv = nhanvien.Tennv;
                existingNhanvien.Ngaysinh = nhanvien.Ngaysinh;
                existingNhanvien.Gioitinh = nhanvien.Gioitinh;
                existingNhanvien.Sdt = nhanvien.Sdt;
                existingNhanvien.Dc = nhanvien.Dc;
                await _nhanvienRepository.UpdateAsync(existingNhanvien);
                return RedirectToAction(nameof(Index));
            }
            return View(nhanvien);
        }

        public async Task<IActionResult> Delete(int manv)
        {
            var nhanvien = await _nhanvienRepository.GetByIdAsync(manv);
            if (nhanvien == null)
            {
                return NotFound();
            }
            return View(nhanvien);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int manv)
        {
            await _nhanvienRepository.DeleteAsync(manv);
            return RedirectToAction(nameof(Index));
        }
    }
}
