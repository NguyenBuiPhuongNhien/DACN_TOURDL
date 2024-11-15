using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DO_AN.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien)]
    public class HuongdanController : Controller
    {
        private readonly IHuongdanRepository _huongdanRepository;
        private readonly INhanvienRepository _nhanvienRepository;
        private readonly ITourRepository _tourRepository;
        private readonly ILoaitourRepository _loaitourRepository;
        private readonly IDiemdenRepository _hiemdenRepository;

        public HuongdanController(IHuongdanRepository huongdanRepository, INhanvienRepository nhanvienRepository, ITourRepository tourRepository, ILoaitourRepository loaitourRepository, IDiemdenRepository hiemdenRepository)
        {
            _huongdanRepository = huongdanRepository;
            _nhanvienRepository = nhanvienRepository;
            _tourRepository = tourRepository;
            _loaitourRepository = loaitourRepository;
            _hiemdenRepository = hiemdenRepository;
        }

        public async Task<IActionResult> Index()
        {
            var huongdans = await _huongdanRepository.GetAllAsync();
            return View(huongdans);
        }

        public async Task<IActionResult> Add()
        {
            var nhanviens = await _nhanvienRepository.GetAllAsync();
            var tours = await _tourRepository.GetAllAsync();
            ViewBag.Nhanviens = new SelectList(nhanviens, "Manv", "Tennv"); // Assuming "TenNv" is a property in Nhanvien
            ViewBag.Tours = new SelectList(tours, "Matour", "Tentour"); // Assuming "Tentour" is a property in Tour
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Huongdan huongdan)
        {
            if (ModelState.IsValid)
            {
                
                var nhanviens = await _nhanvienRepository.GetAllAsync();
                var tours = await _tourRepository.GetAllAsync();
                ViewBag.Nhanviens = new SelectList(nhanviens, "Manv", "TenNv");
                ViewBag.Tours = new SelectList(tours, "Matour", "Tentour");
                return View(huongdan);
                //await _huongdanRepository.AddAsync(huongdan);
                //return RedirectToAction(nameof(Index));
            }
            try
            {
                if (huongdan.Nhiemvu == null)
                    return Json(new { success = false, message = "Nhiệm vụ chưa được cập nhật" });
                await _huongdanRepository.AddAsync(huongdan);
                return Json(new { success = true });
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
                {
                    return Json(new { success = false, message = "Không thể thêm bản ghi trùng lặp." });
                }
                return Json(new { success = false, message = "Đã xảy ra lỗi khi thêm dữ liệu." });
            }

        }

        public async Task<IActionResult> Update(int manv, int matour)
        {
            var huongdan = await _huongdanRepository.GetByIdAsync(manv, matour);
            if (huongdan == null)
            {
                return NotFound();
            }
            var nhanviens = await _nhanvienRepository.GetAllAsync();
            var tours = await _tourRepository.GetAllAsync();
            ViewBag.Nhanviens = new SelectList(nhanviens, "Manv", "TenNv");
            ViewBag.Tours = new SelectList(tours, "Matour", "Tentour");
            return View(huongdan);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int manv, int matour, Huongdan huongdan)
        {
            if (manv != huongdan.Manv || matour != huongdan.Matour)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                await _huongdanRepository.UpdateAsync(huongdan);
                return RedirectToAction(nameof(Index));
            }
            var nhanviens = await _nhanvienRepository.GetAllAsync();
            var tours = await _tourRepository.GetAllAsync();
            ViewBag.Nhanviens = new SelectList(nhanviens, "Manv", "TenNv");
            ViewBag.Tours = new SelectList(tours, "Matour", "Tentour");
            return View(huongdan);
        }

        public async Task<IActionResult> Delete(int manv, int matour)
        {
            var huongdan = await _huongdanRepository.GetByIdAsync(manv, matour);
            if (huongdan == null)
            {
                return NotFound();
            }
            return View(huongdan);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int manv, int matour)
        {
            await _huongdanRepository.DeleteAsync(manv, matour);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Display(int manv, int matour)
        {
            var huongdan = await _huongdanRepository.GetByIdAsync(manv, matour);
            if (huongdan == null)
            {
                return NotFound();
            }
            return View(huongdan);
        }
    }
}
