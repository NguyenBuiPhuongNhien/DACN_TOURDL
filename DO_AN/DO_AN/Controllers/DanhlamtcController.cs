using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DO_AN.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien)]
    public class DanhlamtcController : Controller
    {
        private readonly IDanhlamtcRepository _danhlamtcRepository;
        private readonly IHinhanhRepository _hinhanhRepository;
        private readonly ILoaitourRepository _loaitourRepository;
        private readonly IDiemdenRepository _iemdenRepository;
        

        public DanhlamtcController(IDanhlamtcRepository danhlamtcRepository, IHinhanhRepository hinhanhRepository, IDiemdenRepository diemdenRepository,ILoaitourRepository loaitourRepository)
        {
            _danhlamtcRepository = danhlamtcRepository;
            _hinhanhRepository = hinhanhRepository;
            _iemdenRepository = diemdenRepository;
            _loaitourRepository = loaitourRepository;
        }

        public async Task<IActionResult> Index()
        {
            var danhlamtcs = await _danhlamtcRepository.GetAllAsync();
            return View(danhlamtcs);
        }

        public async Task<IActionResult> AddAsync()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Danhlamtc danhlamtc)
        {
            if (ModelState.IsValid)
            {
                await _danhlamtcRepository.AddAsync(danhlamtc);
                return RedirectToAction(nameof(Index));
            }
            return View(danhlamtc);
        }

        public async Task<IActionResult> Update(int madl)
        {
            var danhlamtc = await _danhlamtcRepository.GetByIdAsync(madl);
            if (danhlamtc == null)
            {
                return NotFound();
            }
            return View(danhlamtc);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int madl, Danhlamtc danhlamtc)
        {
            if (madl != danhlamtc.Madl)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingDanhlamtc = await _danhlamtcRepository.GetByIdAsync(madl);
                existingDanhlamtc.Tendl = danhlamtc.Tendl;
                await _danhlamtcRepository.UpdateAsync(existingDanhlamtc);
                return RedirectToAction(nameof(Index));
            }
            return View(danhlamtc);
        }
        public async Task<IActionResult> Display(int madl)
        {
            var danhlamtc = await _danhlamtcRepository.GetByIdAsync(madl);
            if (danhlamtc == null)
            {
                return NotFound();
            }
            return View(danhlamtc);
        }

        public async Task<IActionResult> Delete(int madl)
        {
            var danhlamtc = await _danhlamtcRepository.GetByIdAsync(madl);
            if (danhlamtc == null)
            {
                return NotFound();
            }
            return View(danhlamtc);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int madl)
        {
            await _danhlamtcRepository.DeleteAsync(madl);
            return RedirectToAction(nameof(Index));
        }
    }
}
