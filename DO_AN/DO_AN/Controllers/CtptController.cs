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
    public class CtptController : Controller
    {
        private readonly ICtptRepository _ctptRepository;
        private readonly IPhuongtiendcRepository _phuongtiendcRepository;
        private readonly ITourRepository _tourRepository;
        private readonly ILoaitourRepository _loaitourRepository;
        private readonly IDiemdenRepository _diemdenRepository;

        public CtptController(ICtptRepository ctptRepository, IPhuongtiendcRepository phuongtiendcRepository, ITourRepository tourRepository, ILoaitourRepository loaitourRepository, IDiemdenRepository diemdenRepository)
        {
            _ctptRepository = ctptRepository;
            _phuongtiendcRepository = phuongtiendcRepository;
            _tourRepository = tourRepository;
            _loaitourRepository = loaitourRepository;
            _diemdenRepository = diemdenRepository;
        }

        public async Task<IActionResult> Index()
        {
            var ctpts = await _ctptRepository.GetAllAsync();
            return View(ctpts);
        }

        public async Task<IActionResult> Add()
        {
            var phuongtiendcs = await _phuongtiendcRepository.GetAllAsync();
            var tours = await _tourRepository.GetAllAsync();
            ViewBag.Phuongtiendcs = new SelectList(phuongtiendcs, "Mapt", "Tenpt");
            ViewBag.Tours = new SelectList(tours, "Matour", "Tentour");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Ctpt ctpt)
        {
            if (ModelState.IsValid)
            {
                var phuongtiendcs = await _phuongtiendcRepository.GetAllAsync();
                var tours = await _tourRepository.GetAllAsync();
                ViewBag.Phuongtiendcs = new SelectList(phuongtiendcs, "Mapt", "Tenpt");
                ViewBag.Tours = new SelectList(tours, "Matour", "Tentour");
                return View(ctpt);
            }

            try
            {
                await _ctptRepository.AddAsync(ctpt);
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

        public async Task<IActionResult> Update(int matour, int mapt)
        {
            var ctpt = await _ctptRepository.GetByIdAsync(matour, mapt);
            if (ctpt == null)
            {
                return NotFound();
            }
            var phuongtiendcs = await _phuongtiendcRepository.GetAllAsync();
            var tours = await _tourRepository.GetAllAsync();
            ViewBag.Phuongtiendcs = new SelectList(phuongtiendcs, "Mapt", "Tenpt");
            ViewBag.Tours = new SelectList(tours, "Matour", "Tentour");
            return View(ctpt);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int matour, int mapt, Ctpt ctpt)
        {
            if (matour != ctpt.Matour || mapt != ctpt.Mapt)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var phuongtiendcs = await _phuongtiendcRepository.GetAllAsync();
                var tours = await _tourRepository.GetAllAsync();
                ViewBag.Phuongtiendcs = new SelectList(phuongtiendcs, "Mapt", "Tenpt");
                ViewBag.Tours = new SelectList(tours, "Matour", "Tentour");
                return View(ctpt);
            }

            
                await _ctptRepository.UpdateAsync(ctpt);
                return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int matour, int mapt)
        {
            var ctpt = await _ctptRepository.GetByIdAsync(matour, mapt);
            if (ctpt == null)
            {
                return NotFound();
            }
            return View(ctpt);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int matour, int mapt)
        {
            await _ctptRepository.DeleteAsync(matour, mapt);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Display(int matour, int mapt)
        {
            var ctpt = await _ctptRepository.GetByIdAsync(matour, mapt);
            if (ctpt == null)
            {
                return NotFound();
            }
            return View(ctpt);
        }
    }
}
