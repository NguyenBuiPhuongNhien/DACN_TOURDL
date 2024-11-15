using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DO_AN.Controllers
{
    [Authorize(Roles = SD.Role_NhanVien + "," + SD.Role_Admin)]
    public class DiemkhoihanhController : Controller
    {
        private readonly IDiemkhoihanhRepository _diemkhoihanhRepository;
        private readonly IDiemdenRepository _diemdenRepository;
        private readonly ILoaitourRepository _loaitourRepository;
        public DiemkhoihanhController(IDiemkhoihanhRepository diemkhoihanhRepository, IDiemdenRepository diemdenRepository, ILoaitourRepository loaitourRepository)
        {
            _diemkhoihanhRepository = diemkhoihanhRepository;
            _diemdenRepository = diemdenRepository;
            _loaitourRepository = loaitourRepository;
        }
        public async Task<IActionResult> index()
        {
            var diemkhoihanhs = await _diemkhoihanhRepository.GetAllAsync();
            return View(diemkhoihanhs);
        }
        
        public async Task<IActionResult> Add()
        {
            return View();
        }
        // Xử lý thêm sản phẩm mới
        [HttpPost]
        public async Task<IActionResult> Add(Diemkhoihanh diemkhoihanh)
        {
            if (ModelState.IsValid)
            {
                await _diemkhoihanhRepository.AddAsync(diemkhoihanh);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            return View(diemkhoihanh);
        }
        // Viết thêm hàm SaveImage (tham khảo bài 02)
       
        // Hiển thị form cập nhật sản phẩm
        public async Task<IActionResult> Update(int madkh)
        {
            var diemkhoihanh = await _diemkhoihanhRepository.GetByIdAsync(madkh);
            if (diemkhoihanh == null)
            {
                return NotFound();
            }
            return View(diemkhoihanh);
        }
        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int madkh, Diemkhoihanh diemkhoihanh)
        {
            if (madkh != diemkhoihanh.Madkh)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingDiemkhoihanh = await _diemkhoihanhRepository.GetByIdAsync(madkh);
                existingDiemkhoihanh.Tendkh = diemkhoihanh.Tendkh;
                existingDiemkhoihanh.Dc = diemkhoihanh.Dc;
                existingDiemkhoihanh.Sdt = diemkhoihanh.Sdt;
                await _diemkhoihanhRepository.UpdateAsync(existingDiemkhoihanh);
                return RedirectToAction(nameof(Index));
            }
            return View(diemkhoihanh);
        }
        // Hiển thị form xác nhận xóa sản phẩm
        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int madkh)
        {
            var diemkhoihanh = await _diemkhoihanhRepository.GetByIdAsync(madkh);
            if (diemkhoihanh == null)
            {
                return NotFound();
            }
            return View(diemkhoihanh);
        }
        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int madkh)
        {
            await _diemkhoihanhRepository.DeleteAsync(madkh);
            return RedirectToAction(nameof(Index));
        }
    }
}

