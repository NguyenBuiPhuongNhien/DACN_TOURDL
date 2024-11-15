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

    public class CtddController : Controller
    {
        private readonly ICtddRepository _ctddRepository;
        private readonly IDiemthamquanRepository _diemthamquanRepository;
        private readonly IKhachsanRepository _khachsanRepository;
        private readonly ILichtrinhRepository _lichtrinhRepository;
        private readonly ILoaitourRepository _loaitourRepository;
        private readonly IDiemdenRepository _diemdenRepository;
        private readonly ITourRepository _tourRepository;
        private readonly ICtDtqRepository _ctDtqRepository;
        private readonly IHinhanhRepository _hinhanhRepository;

        public CtddController(ICtddRepository ctddRepository, IDiemthamquanRepository diemthamquanRepository, IKhachsanRepository khachsanRepository, ILichtrinhRepository lichtrinhRepository, ILoaitourRepository loaitourRepository, IDiemdenRepository diemdenRepository, ITourRepository tourRepository, ICtDtqRepository ctDtqRepository, IHinhanhRepository hinhanhRepository)
        {
            _ctddRepository = ctddRepository;
            _diemthamquanRepository = diemthamquanRepository;
            _khachsanRepository = khachsanRepository;
            _lichtrinhRepository = lichtrinhRepository;
            _loaitourRepository = loaitourRepository;
            _diemdenRepository = diemdenRepository;
            _tourRepository = tourRepository;
            _ctDtqRepository = ctDtqRepository;
            _hinhanhRepository = hinhanhRepository;
        }
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien)]
        public async Task<IActionResult> Index()
        {

            var ctdds = await _ctddRepository.GetAllAsync();
            return View(ctdds);
        }
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien)]
        public async Task<IActionResult> Add()
        {

            var diemthamquan = await _diemthamquanRepository.GetAllAsync();
            var khachsan = await _khachsanRepository.GetAllAsync();
            var lichtrinh = await _lichtrinhRepository.GetAllAsync();
            ViewBag.Diemthamquan = new SelectList(diemthamquan, "Madtq", "Tendtq");
            ViewBag.Khachsan = new SelectList(khachsan, "Maks", "Tenks");
            ViewBag.Lichtrinh = new SelectList(lichtrinh, "Malt", "Tenlt");
            return View();
        }
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien)]
        [HttpPost]
        public async Task<IActionResult> Add(Ctdd ctdd)
        {
            var diemthamquan = await _diemthamquanRepository.GetAllAsync();
            var khachsan = await _khachsanRepository.GetAllAsync();
            var lichtrinh = await _lichtrinhRepository.GetAllAsync();
            if (ModelState.IsValid)
            {
                ViewBag.Diemthamquan = new SelectList(diemthamquan, "Madtq", "Tendtq");
                ViewBag.Khachsan = new SelectList(khachsan, "Maks", "Tenks");
                ViewBag.Lichtrinh = new SelectList(lichtrinh, "Malt", "Tenlt");
                ViewData["Lichtrinh"] = lichtrinh;

                return View(ctdd);
                //await _ctddRepository.AddAsync(ctdd);
                //return RedirectToAction(nameof(Index));
            }
            try
            {
                await _ctddRepository.AddAsync(ctdd);
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
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien)]
        public async Task<IActionResult> Update(int malt, int maks, int madtq)
        {
            var ctdd = await _ctddRepository.GetByIdAsync(malt, maks, madtq);
            if (ctdd == null)
            {
                return NotFound();
            }
            var diemthamquan = await _diemthamquanRepository.GetAllAsync();
            var khachsan = await _khachsanRepository.GetAllAsync();
            var lichtrinh = await _lichtrinhRepository.GetAllAsync();
            ViewBag.Diemthamquan = new SelectList(diemthamquan, "Madtq", "Tendtq");
            ViewBag.Khachsan = new SelectList(khachsan, "Maks", "Tenks");
            ViewBag.Lichtrinh = new SelectList(lichtrinh, "Malt", "Tenlt");
            return View(ctdd);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int malt, int maks, int madtq, Ctdd ctdd)
        {
            if (malt != ctdd.Malt || maks != ctdd.Maks || madtq != ctdd.Madtq)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                await _ctddRepository.UpdateAsync(ctdd);
                return RedirectToAction(nameof(Index));
            }
            var diemthamquan = await _diemthamquanRepository.GetAllAsync();
            var khachsan = await _khachsanRepository.GetAllAsync();
            var lichtrinh = await _lichtrinhRepository.GetAllAsync();
            ViewBag.Diemthamquan = new SelectList(diemthamquan, "Madtq", "Tendtq");
            ViewBag.Khachsan = new SelectList(khachsan, "Maks", "Tenks");
            ViewBag.Lichtrinh = new SelectList(lichtrinh, "Malt", "Tenlt");
            return View(ctdd);
        }
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien)]
        public async Task<IActionResult> Delete(int malt, int maks, int madtq)
        {
            var ctdd = await _ctddRepository.GetByIdAsync(malt, maks, madtq);
            if (ctdd == null)
            {
                return NotFound();
            }
            return View(ctdd);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int malt, int maks, int madtq)
        {
            await _ctddRepository.DeleteAsync(malt, maks, madtq);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Display(int malt, int maks, int madtq)
        {
            var ctdd = await _ctddRepository.GetByIdAsync(malt, maks, madtq);
            if (ctdd == null)
            {
                return NotFound();
            }
            return View(ctdd);
        }

        // Các action Update, Delete, Display tương tự như trong CtptController.
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien+ "," + SD.Role_Khachhang)]
        public async Task<IActionResult> Detail(int matour)
        {
            var tour = await _tourRepository.GetByIdAsync(matour);
            var ctdd = await _ctddRepository.GetallMaltAsync(tour.Malt);
            var ctdtq = await _ctDtqRepository.GetAllAsync();
            var diemthamquan = await _diemthamquanRepository.GetAllAsync();
            var hinhanh = await _hinhanhRepository.GetAllAsync();

            if (ctdd == null)
            {
                return NotFound();
            }
            ViewData["Tour"] = tour;
            ViewData["CtDtq"] = ctdtq;
            ViewData["Hinhanh"] = hinhanh;
            
            return View(ctdd);
        }
    }
}
