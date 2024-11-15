using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DO_AN.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien + "," + SD.Role_Khachhang)]
    public class LichtrinhController : Controller
    {
        private readonly ILichtrinhRepository _lichtrinhRepository;
        private readonly ILoaitourRepository _loaitourRepository;
        private readonly IDiemdenRepository _diemdenRepository;

        public LichtrinhController(ILichtrinhRepository lichtrinhRepository, ILoaitourRepository loaitourRepository, IDiemdenRepository diemdenRepository)
        {
            _lichtrinhRepository = lichtrinhRepository;
            _loaitourRepository = loaitourRepository;
            _diemdenRepository = diemdenRepository;
        }

        public async Task<IActionResult> Index()
        {
            var lichtrinhs = await _lichtrinhRepository.GetAllAsync();
            return View(lichtrinhs);
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Lichtrinh lichtrinh)
        {
            if (ModelState.IsValid)
            {
                await _lichtrinhRepository.AddAsync(lichtrinh);
                return RedirectToAction(nameof(Index));
            }
            return View(lichtrinh);
        }

        public async Task<IActionResult> Update(int malt)
        {
            var lichtrinh = await _lichtrinhRepository.GetByIdAsync(malt);
            if (lichtrinh == null)
            {
                return NotFound();
            }
            return View(lichtrinh);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int malt, Lichtrinh lichtrinh)
        {
            if (malt != lichtrinh.Malt)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _lichtrinhRepository.UpdateAsync(lichtrinh);
                return RedirectToAction(nameof(Index));
            }
            return View(lichtrinh);
        }

        public async Task<IActionResult> Delete(int malt)
        {
            var lichtrinh = await _lichtrinhRepository.GetByIdAsync(malt);
            if (lichtrinh == null)
            {
                return NotFound();
            }
            return View(lichtrinh);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int malt)
        {
            await _lichtrinhRepository.DeleteAsync(malt);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Display(int malt)
        {
            var lichtrinh = await _lichtrinhRepository.GetByIdAsync(malt);
            if (lichtrinh == null)
            {
                return NotFound();
            }
            return View(lichtrinh);
        }
    }
}
