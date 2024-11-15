using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DO_AN.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien)]
    public class DiemdenController : Controller
    {
        private readonly IDiemdenRepository _diemdenRepository;
        private readonly ILoaitourRepository _loaitourRepository;
        public DiemdenController(IDiemdenRepository diemdenRepository, ILoaitourRepository loaitourRepository)
        {
            _diemdenRepository = diemdenRepository;
            _loaitourRepository = loaitourRepository;
        }

        public async Task<IActionResult> Index()
        {
            var diemdens = await _diemdenRepository.GetAllAsync();
            return View(diemdens);
        }

        public async Task<IActionResult> AddAsync()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Diemden diemden)
        {
            if (ModelState.IsValid)
            {
                await _diemdenRepository.AddAsync(diemden);
                return RedirectToAction(nameof(Index));
            }
            return View(diemden);
        }

        public async Task<IActionResult> Update(int madd)
        {
            var diemden = await _diemdenRepository.GetByIdAsync(madd);
            if (diemden == null)
            {
                return NotFound();
            }
            return View(diemden);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int madd, Diemden diemden)
        {
            if (madd != diemden.Madd)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingDiemden = await _diemdenRepository.GetByIdAsync(madd);
                existingDiemden.Tendd = diemden.Tendd;
                await _diemdenRepository.UpdateAsync(existingDiemden);
                return RedirectToAction(nameof(Index));
            }
            return View(diemden);
        }

        public async Task<IActionResult> Delete(int madd)
        {
            var diemden = await _diemdenRepository.GetByIdAsync(madd);
            if (diemden == null)
            {
                return NotFound();
            }
            return View(diemden);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int madd)
        {
            await _diemdenRepository.DeleteAsync(madd);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Display(int madd)
        {
            var diemden = await _diemdenRepository.GetByIdAsync(madd);
            if (diemden == null)
            {
                return NotFound();
            }
            return View(diemden);
        }
    }
}
