using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace DO_AN.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien)]
    public class KhachsanController : Controller
    {
        private readonly IKhachsanRepository _khachsanRepository;
        private readonly IDiemdenRepository _diemdenRepository;
        private readonly ILoaitourRepository _loaitourRepository;

        public KhachsanController(IKhachsanRepository khachsanRepository, IDiemdenRepository diemdenRepository, ILoaitourRepository loaitourRepository)
        {
            _khachsanRepository = khachsanRepository;
            _diemdenRepository = diemdenRepository;
            _loaitourRepository = loaitourRepository;
        }

        public async Task<IActionResult> Index()
        {
            var khachsans = await _khachsanRepository.GetAllAsync();
            return View(khachsans);
        }

        public async Task<IActionResult> Add()
        {
            var diemdens = await _diemdenRepository.GetAllAsync();
            ViewBag.Diemdens = new SelectList(diemdens, "Madd", "Tendd");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Khachsan khachsan)
        {
            if (!ModelState.IsValid)
            {
                await _khachsanRepository.AddAsync(khachsan);
                return RedirectToAction(nameof(Index));
            }
            var diemdens = await _diemdenRepository.GetAllAsync();
            ViewBag.Diemdens = new SelectList(diemdens, "Madd", "Tendd");
            return View(khachsan);
        }

        public async Task<IActionResult> Update(int maks)
        {
            var khachsan = await _khachsanRepository.GetByIdAsync(maks);
            if (khachsan == null)
            {
                return NotFound();
            }
            var diemdens = await _diemdenRepository.GetAllAsync();
            ViewBag.Diemdens = new SelectList(diemdens, "Madd", "Tendd");
            return View(khachsan);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int maks, Khachsan khachsan)
        {
            if (maks != khachsan.Maks)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                var existingKhachsan = await _khachsanRepository.GetByIdAsync(maks);
                existingKhachsan.Tenks = khachsan.Tenks;
                existingKhachsan.Dc = khachsan.Dc;
                await _khachsanRepository.UpdateAsync(existingKhachsan);
                return RedirectToAction(nameof(Index));
            }
            var diemdens = await _diemdenRepository.GetAllAsync();
            ViewBag.Diemdens = new SelectList(diemdens, "Madd", "Tendd");
            return View(khachsan);
        }

        public async Task<IActionResult> Delete(int maks)
        {
            var khachsan = await _khachsanRepository.GetByIdAsync(maks);
            if (khachsan == null)
            {
                return NotFound();
            }
            return View(khachsan);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int maks)
        {
            await _khachsanRepository.DeleteAsync(maks);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Display(int maks)
        {
            var khachsan = await _khachsanRepository.GetByIdAsync(maks);
            if (khachsan == null)
            {
                return NotFound();
            }
            return View(khachsan);
        }
    }
}
