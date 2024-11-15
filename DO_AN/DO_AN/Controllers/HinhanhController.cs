using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace DO_AN.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien)]
    public class HinhanhController : Controller
    {
        private readonly IHinhanhRepository _hinhanhRepository;
        private readonly IDanhlamtcRepository _danhlamtcRepository;
        private readonly IDiemdenRepository _diemdenRepository;
        private readonly ILoaitourRepository _loaitourRepository;

        public HinhanhController(IHinhanhRepository hinhanhRepository, IDanhlamtcRepository danhlamtcRepository, IDiemdenRepository diemdenRepository, ILoaitourRepository loaitourRepository)
        {
            _hinhanhRepository = hinhanhRepository;
            _danhlamtcRepository = danhlamtcRepository;
            _diemdenRepository = diemdenRepository;
            _loaitourRepository = loaitourRepository;
        }

        public async Task<IActionResult> Index()
        {
            var hinhanhs = await _hinhanhRepository.GetAllAsync();
            return View(hinhanhs);
        }

        public async Task<IActionResult> Add()
        {
            var danhlamtcs = await _danhlamtcRepository.GetAllAsync();
            ViewBag.Danhlamtcs = new SelectList(danhlamtcs, "Madl","Tendl");
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(Hinhanh hinhanh, IFormFile url)
        {
            if (!ModelState.IsValid)
            {
                if (url != null)
                {
                    // Lưu hình ảnh đại diện tham khảo bài 02 hàm SaveImage

                    hinhanh.Url = await SaveImage(url);

                }
                await _hinhanhRepository.AddAsync(hinhanh);
                return RedirectToAction(nameof(Index));
            }
            var danhlamtcs = await _danhlamtcRepository.GetAllAsync();
            ViewBag.Danhlamtcs = new SelectList(danhlamtcs, "Madl", "Tendl");
            return View(hinhanh);
        }

        private async Task<string> SaveImage(IFormFile url)
        {
            var savePath = Path.Combine("wwwroot/images", url.FileName); //Thay đổi đường dẫn theo cấu hình của bạn
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await url.CopyToAsync(fileStream);
            }
            return "/images/" + url.FileName; // Trả về đường dẫn tương đối
        }
        public async Task<IActionResult> Update(int mah)
        {
            var hinhanh = await _hinhanhRepository.GetByIdAsync(mah);
            if (hinhanh == null)
            {
                return NotFound();
            }
            var danhlamtcs = await _danhlamtcRepository.GetAllAsync();
            ViewBag.Danhlamtcs = new SelectList(danhlamtcs, "Madl", "Tendl");
            return View(hinhanh);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int mah, Hinhanh hinhanh, IFormFile url)
        {
            if (mah != hinhanh.Mah)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var existingHinhanh = await _hinhanhRepository.GetByIdAsync(mah);

                if (url == null)
                {
                    // Không có hình ảnh mới được tải lên, giữ nguyên Url cũ
                    hinhanh.Url = existingHinhanh.Url;
                }
                else
                {
                    // Có hình ảnh mới được tải lên, lưu hình ảnh mới và cập nhật Url
                    existingHinhanh.Url = await SaveImage(url);
                }

                    existingHinhanh.Madl = hinhanh.Madl;

                await _hinhanhRepository.UpdateAsync(existingHinhanh);
                return RedirectToAction(nameof(Index));
            }

            var danhlamtcs = await _danhlamtcRepository.GetAllAsync();
            ViewBag.Danhlamtcs = new SelectList(danhlamtcs, "Madl", "Tendl");
            return View(hinhanh);
        }

        public async Task<IActionResult> Delete(int mah)
        {
            var hinhanh = await _hinhanhRepository.GetByIdAsync(mah);
            if (hinhanh == null)
            {
                return NotFound();
            }
            return View(hinhanh);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int mah)
        {
            await _hinhanhRepository.DeleteAsync(mah);
            return RedirectToAction(nameof(Index));
        }
    }
}
