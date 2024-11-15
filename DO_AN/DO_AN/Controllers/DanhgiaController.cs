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
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien + "," + SD.Role_Khachhang)]
    public class DanhgiaController : Controller
    {
        private readonly IDanhgiaRepository _danhgiaRepository;
        private readonly IKhachhangRepository _khachhangRepository;
        private readonly ITourRepository _tourRepository;
        private readonly IDiemdenRepository _diemdenRepository;
        private readonly ILoaitourRepository _loaitourRepository;

        public DanhgiaController(IDanhgiaRepository danhgiaRepository, IKhachhangRepository khachhangRepository, ITourRepository tourRepository, IDiemdenRepository diemdenRepository, ILoaitourRepository loaitourRepository) { 
            _danhgiaRepository = danhgiaRepository;
            _khachhangRepository = khachhangRepository;
            _tourRepository = tourRepository;
            _diemdenRepository = diemdenRepository;
            _loaitourRepository = loaitourRepository;
        }

        public async Task<IActionResult> Index()
        {
            var danhgias = await _danhgiaRepository.GetAllAsync();
            return View(danhgias);
        }

        public async Task<IActionResult> Add()
        {
            var khachhangs = await _khachhangRepository.GetAllAsync();
            var tours = await _tourRepository.GetAllAsync();
            ViewBag.Khachhangs = new SelectList(khachhangs, "Makh", "Tenkh");
            ViewBag.Tours = new SelectList(tours, "Matour", "Tentour");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Danhgia danhgia)
        {
            if (ModelState.IsValid)
            {
                var khachhangs = await _khachhangRepository.GetAllAsync();
                var tours = await _tourRepository.GetAllAsync();
                ViewBag.Khachhangs = new SelectList(khachhangs, "Makh", "Tenkh");
                ViewBag.Tours = new SelectList(tours, "Matour", "Tentour");
                return View(danhgia);       
            }
            danhgia.Thoigiandanhgia = DateOnly.FromDateTime(DateTime.Now);

            try
            {
                await _danhgiaRepository.AddAsync(danhgia);
                return Json(new { success = true });
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
                {
                    return Json(new { success = false, message = "Bạn đã sai. Không thể thêm bản ghi trùng lặp." });
                }
                return Json(new { success = false, message = "Đã xảy ra lỗi khi thêm dữ liệu." });
            }
        }

        public async Task<IActionResult> Update(int makh, int matour)
        {
            var danhgia = await _danhgiaRepository.GetByIdAsync(makh, matour);
            if (danhgia == null)
            {
                return NotFound();
            }
            
            var khachhangs = await _khachhangRepository.GetAllAsync();
            var tours = await _tourRepository.GetAllAsync();
            ViewBag.Khachhangs = new SelectList(khachhangs, "Makh", "Tenkh");
            ViewBag.Tours = new SelectList(tours, "Matour", "Tentour");
            return View(danhgia);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int makh, int matour, Danhgia danhgia)
        {
            var khachhangs = await _khachhangRepository.GetAllAsync();
            var tours = await _tourRepository.GetAllAsync();
            if (makh != danhgia.Makh || matour != danhgia.Matour)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                danhgia.Thoigiandanhgia = DateOnly.FromDateTime(DateTime.Now);
                await _danhgiaRepository.UpdateAsync(danhgia);
                return RedirectToAction(nameof(Index));
                
            }
            ViewBag.Khachhangs = new SelectList(khachhangs, "Makh", "Tenkh");
            ViewBag.Tours = new SelectList(tours, "Matour", "Tentour");
            return View(danhgia);
        }

        public async Task<IActionResult> Delete(int makh, int matour)
        {
            var danhgia = await _danhgiaRepository.GetByIdAsync(makh, matour);
            if (danhgia == null)
            {
                return NotFound();
            }
            return View(danhgia);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int makh, int matour)
        {
            await _danhgiaRepository.DeleteAsync(makh, matour);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Display(int makh, int matour)
        {
            var danhgia = await _danhgiaRepository.GetByIdAsync(makh, matour);
            if (danhgia == null)
            {
                return NotFound();
            }
            return View(danhgia);
        }
    }
}
