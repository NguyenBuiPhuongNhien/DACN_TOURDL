using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace DO_AN.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien)]
    public class DiemthamquanController : Controller
    {
        private readonly IDiemthamquanRepository _diemthamquanRepository;
        private readonly IDiemdenRepository _diemdenRepository;
        private readonly ILoaitourRepository _loaitourRepository;

        public DiemthamquanController(IDiemthamquanRepository diemthamquanRepository, IDiemdenRepository diemdenRepository, ILoaitourRepository loaitourRepository)
        {
            _diemthamquanRepository = diemthamquanRepository;
            _diemdenRepository = diemdenRepository;
            _loaitourRepository = loaitourRepository;
        }

        public async Task<IActionResult> Index()
        {
            var diemthamquans = await _diemthamquanRepository.GetAllAsync();
            return View(diemthamquans);
        }

        public async Task<IActionResult> Add()
        {
            var diemdens = await _diemdenRepository.GetAllAsync();
            ViewBag.Diemdens = new SelectList(diemdens, "Madd", "Tendd");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Diemthamquan diemthamquan)
        {
            if (!ModelState.IsValid)
            {
                await _diemthamquanRepository.AddAsync(diemthamquan);
                return RedirectToAction(nameof(Index));
            }
            var diemdens = await _diemdenRepository.GetAllAsync();
            ViewBag.Diemdens = new SelectList(diemdens, "Madd", "Tendd");
            return View(diemthamquan);
        }

        public async Task<IActionResult> Update(int madtq)
        {
            var diemthamquan = await _diemthamquanRepository.GetByIdAsync(madtq);
            if (diemthamquan == null)
            {
                return NotFound();
            }
            var diemdens = await _diemdenRepository.GetAllAsync();
            ViewBag.Diemdens = new SelectList(diemdens, "Madd", "Tendd");
            return View(diemthamquan);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int madtq, Diemthamquan diemthamquan)
        {
            if (madtq != diemthamquan.Madtq)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                var existingDiemthamquan = await _diemthamquanRepository.GetByIdAsync(madtq);
                existingDiemthamquan.Tendtq = diemthamquan.Tendtq;
                existingDiemthamquan.Thongtinct = diemthamquan.Thongtinct;
                await _diemthamquanRepository.UpdateAsync(existingDiemthamquan);
                return RedirectToAction(nameof(Index));
            }
            var diemdens = await _diemdenRepository.GetAllAsync();
            ViewBag.Diemdens = new SelectList(diemdens, "Madd", "Tendd");
            return View(diemthamquan);
        }

        public async Task<IActionResult> Delete(int madtq)
        {
            var diemthamquan = await _diemthamquanRepository.GetByIdAsync(madtq);
            if (diemthamquan == null)
            {
                return NotFound();
            }
            return View(diemthamquan);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int madtq)
        {
            await _diemthamquanRepository.DeleteAsync(madtq);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Display(int madtq)
        {
            var diemthamquan = await _diemthamquanRepository.GetByIdAsync(madtq);
            if (diemthamquan == null)
            {
                return NotFound();
            }
            return View(diemthamquan);
        }
    }
}
