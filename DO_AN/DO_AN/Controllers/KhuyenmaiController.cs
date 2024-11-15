using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DO_AN.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien)]
    public class KhuyenmaiController : Controller
    {
        private readonly IKhuyenmaiRepository _khuyenmaiRepository;
        private readonly ILoaitourRepository _loaitourRepository;
        private readonly IDiemdenRepository _diemdenRepository;

        public KhuyenmaiController(IKhuyenmaiRepository khuyenmaiRepository, ILoaitourRepository loaitourRepository, IDiemdenRepository diemdenRepository)
        {
            _khuyenmaiRepository = khuyenmaiRepository;
            _loaitourRepository = loaitourRepository;
            _diemdenRepository = diemdenRepository;
        }

        public async Task<IActionResult> Index()
        {
            var khuyenmais = await _khuyenmaiRepository.GetAllAsync();
            return View(khuyenmais);
        }

        public async Task<IActionResult> AddAsync()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Khuyenmai khuyenmai)
        {
            if (ModelState.IsValid)
            {
                await _khuyenmaiRepository.AddAsync(khuyenmai);
                return RedirectToAction(nameof(Index));
            }
            return View(khuyenmai);
        }

        public async Task<IActionResult> Update(string makm)
        {
            var khuyenmai = await _khuyenmaiRepository.GetByIdAsync(makm);
            if (khuyenmai == null)
            {
                return NotFound();
            }
            return View(khuyenmai);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string makm, Khuyenmai khuyenmai)
        {
            if (makm != khuyenmai.Makm)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingKhuyenmai = await _khuyenmaiRepository.GetByIdAsync(makm);
                existingKhuyenmai.Phantramkm = khuyenmai.Phantramkm;
                existingKhuyenmai.Tenkm = khuyenmai.Tenkm;
                existingKhuyenmai.Dk = khuyenmai.Dk;
                await _khuyenmaiRepository.UpdateAsync(existingKhuyenmai);
                return RedirectToAction(nameof(Index));
            }
            return View(khuyenmai);
        }

        public async Task<IActionResult> Delete(string makm)
        {
            var khuyenmai = await _khuyenmaiRepository.GetByIdAsync(makm);
            if (khuyenmai == null)
            {
                return NotFound();
            }
            return View(khuyenmai);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(string makm)
        {
            await _khuyenmaiRepository.DeleteAsync(makm);
            return RedirectToAction(nameof(Index));
        }
    }
}
