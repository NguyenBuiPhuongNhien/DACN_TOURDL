using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DO_AN.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien)]
    public class PhuongtiendcController : Controller
    {
        private readonly IPhuongtiendcRepository _phuongtiendcRepository;
        private readonly ILoaitourRepository _loaitourRepository;
        private readonly IDiemdenRepository _diemdenRepository;

        public PhuongtiendcController(IPhuongtiendcRepository phuongtiendcRepository, ILoaitourRepository loaitourRepository, IDiemdenRepository diemdenRepository)
        {
            _phuongtiendcRepository = phuongtiendcRepository;
            _loaitourRepository = loaitourRepository;
            _diemdenRepository = diemdenRepository;
        }

        public async Task<IActionResult> Index()
        {
            var phuongtiendcs = await _phuongtiendcRepository.GetAllAsync();
            return View(phuongtiendcs);
        }

        public async Task<IActionResult> AddAsync()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Phuongtiendc phuongtiendc)
        {
            if (ModelState.IsValid)
            {
                await _phuongtiendcRepository.AddAsync(phuongtiendc);
                return RedirectToAction(nameof(Index));
            }
            return View(phuongtiendc);
        }

        public async Task<IActionResult> Update(int mapt)
        {
            var phuongtiendc = await _phuongtiendcRepository.GetByIdAsync(mapt);
            if (phuongtiendc == null)
            {
                return NotFound();
            }
            return View(phuongtiendc);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int mapt, Phuongtiendc phuongtiendc)
        {
            if (mapt != phuongtiendc.Mapt)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingPhuongtiendc = await _phuongtiendcRepository.GetByIdAsync(mapt);
                existingPhuongtiendc.Tenpt = phuongtiendc.Tenpt;
                await _phuongtiendcRepository.UpdateAsync(existingPhuongtiendc);
                return RedirectToAction(nameof(Index));
            }
            return View(phuongtiendc);
        }
        public async Task<IActionResult> Delete(int mapt)
        {
            var phuongtiendc = await _phuongtiendcRepository.GetByIdAsync(mapt);
            if (phuongtiendc == null)
            {
                return NotFound();
            }
            return View(phuongtiendc);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int mapt)
        {
            await _phuongtiendcRepository.DeleteAsync(mapt);
            return RedirectToAction(nameof(Index));
        }

    }
}
