using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DO_AN.Controllers
{
    [Authorize(Roles =SD.Role_Admin + "," + SD.Role_NhanVien)]
    public class LoaitourController : Controller
    {
        private readonly ILoaitourRepository _loaitourRepository;
        private readonly IDiemdenRepository diemdenRepository;

        public LoaitourController(ILoaitourRepository loaitourRepository, IDiemdenRepository diemdenRepository)
        {
            _loaitourRepository = loaitourRepository;
            this.diemdenRepository = diemdenRepository;
        }

        public async Task<IActionResult> Index()
        {
           
            var loaitours = await _loaitourRepository.GetAllAsync();
            return View(loaitours);
        }

        public async Task<IActionResult> AddAsync()
        {
          
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Loaitour loaitour)
        {
          
            if (ModelState.IsValid)
            {
                await _loaitourRepository.AddAsync(loaitour);
                return RedirectToAction(nameof(Index));
            }
            return View(loaitour);
        }

        public async Task<IActionResult> Update(int maloai)
        {
          
            var loaitour = await _loaitourRepository.GetByIdAsync(maloai);
            if (loaitour == null)
            {
                return NotFound();
            }
            return View(loaitour);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int maloai, Loaitour loaitour)
        {
           
            if (maloai != loaitour.Maloai)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingLoaitour = await _loaitourRepository.GetByIdAsync(maloai);
                existingLoaitour.Tenloai = loaitour.Tenloai;
                await _loaitourRepository.UpdateAsync(existingLoaitour);
                return RedirectToAction(nameof(Index));
            }
            return View(loaitour);
        }

        public async Task<IActionResult> Delete(int maloai)
        {
            
            var loaitour = await _loaitourRepository.GetByIdAsync(maloai);
            if (loaitour == null)
            {
                return NotFound();
            }
            return View(loaitour);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int maloai)
        {
            
            await _loaitourRepository.DeleteAsync(maloai);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Display(int maloai)
        {
            var loaitour = await _loaitourRepository.GetByIdAsync(maloai);
            if (loaitour == null)
            {
                return NotFound();
            }
            return View(loaitour);
        }
    }
}
