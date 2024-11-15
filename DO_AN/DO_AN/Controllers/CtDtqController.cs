using System.Collections.Generic;
using System.Threading.Tasks;
using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DO_AN.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien)]
    public class CtDtqController : Controller
    {
        private readonly ICtDtqRepository _ctDtqRepository;
        private readonly IDanhlamtcRepository _danhlamtcRepository;
        private readonly IDiemthamquanRepository _diemthamquanRepository;
        private readonly IDiemdenRepository _diemdenRepository;
        private readonly ILoaitourRepository _loaitourRepository;

        public CtDtqController(ICtDtqRepository ctDtqRepository, IDanhlamtcRepository danhlamtcRepository, IDiemthamquanRepository diemthamquanRepository, ILoaitourRepository loaitourRepository, IDiemdenRepository diemdenRepository)
        {
            _ctDtqRepository = ctDtqRepository;
            _danhlamtcRepository = danhlamtcRepository;
            _diemthamquanRepository = diemthamquanRepository;
            _loaitourRepository = loaitourRepository;
            _diemdenRepository = diemdenRepository;
        }

        public async Task<IActionResult> Index()
        {

            var ctdtqs = await _ctDtqRepository.GetAllAsync();
            return View(ctdtqs);
        }

        public async Task<IActionResult> Add()
        {

            var danhlamtc = await _danhlamtcRepository.GetAllAsync();
            var diemthamquan = await _diemthamquanRepository.GetAllAsync();
            ViewBag.Danhlamtc = new SelectList(danhlamtc, "Madl", "Tendl");
            ViewBag.Diemthamquan = new SelectList(diemthamquan, "Madtq", "Tendtq");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CtDtq ctdtq)
        {
            if (ModelState.IsValid)
            {
                var danhlamtc = await _danhlamtcRepository.GetAllAsync();
                var diemthamquan = await _diemthamquanRepository.GetAllAsync();
                ViewBag.Danhlamtc = new SelectList(danhlamtc, "Madl", "Tendl");
                ViewBag.Diemthamquan = new SelectList(diemthamquan, "Madtq", "Tendtq");
                return View(ctdtq);
                //await _ctDtqRepository.AddAsync(ctdtq);
                //return RedirectToAction(nameof(Index));
            }
            try
            {
                await _ctDtqRepository.AddAsync(ctdtq);
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

        public async Task<IActionResult> Update(int madtq, int madl)
        {
            var ctdtq = await _ctDtqRepository.GetByIdAsync(madtq, madl);
            if (ctdtq == null)
            {
                return NotFound();
            }
            var danhlamtc = await _danhlamtcRepository.GetAllAsync();
            var diemthamquan = await _diemthamquanRepository.GetAllAsync();
            ViewBag.Danhlamtc = new SelectList(danhlamtc, "Madl", "Tendl");
            ViewBag.Diemthamquan = new SelectList(diemthamquan, "Madtq", "Tendtq");
            return View(ctdtq);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int madtq, int madl, CtDtq ctdtq)
        {
            if (madtq != ctdtq.Madtq || madl != ctdtq.Madl)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                await _ctDtqRepository.UpdateAsync(ctdtq);
                return RedirectToAction(nameof(Index));
            }
            var danhlamtc = await _danhlamtcRepository.GetAllAsync();
            var diemthamquan = await _diemthamquanRepository.GetAllAsync();
            ViewBag.Danhlamtc = new SelectList(danhlamtc, "Madl", "Tendl");
            ViewBag.Diemthamquan = new SelectList(diemthamquan, "Madtq", "Tendtq");
            return View(ctdtq);
        }

        public async Task<IActionResult> Delete(int madtq, int madl)
        {
            var ctdtq = await _ctDtqRepository.GetByIdAsync(madtq, madl);
            if (ctdtq == null)
            {
                return NotFound();
            }
            return View(ctdtq);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int madtq, int madl)
        {
            await _ctDtqRepository.DeleteAsync(madtq, madl);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Display(int madtq, int madl)
        {
            var ctdtq = await _ctDtqRepository.GetByIdAsync(madtq, madl);
            if (ctdtq == null)
            {
                return NotFound();
            }
            return View(ctdtq);
        }
    }
}
