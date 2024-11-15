using System.Collections.Generic;
using System.Threading.Tasks;
using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static DO_AN.Models.VnPayment;



namespace DO_AN.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien + "," + SD.Role_Khachhang)]
    public class PhieudattourController : Controller
    {
        private readonly IPhieudattourRepository _phieudattourRepository;
        private readonly IKhachhangRepository _khachhangRepository;
        private readonly IKhuyenmaiRepository _khuyenmaiRepository;
        private readonly ITourRepository _tourRepository;
        private readonly ILoaitourRepository _loaitourRepository;
        private readonly IDiemdenRepository _hiemdenRepository;
        private readonly IVnPayRepository _vnPayRepository;
        private readonly UserManager<User> _userManager;

        public PhieudattourController(IPhieudattourRepository phieudattourRepository, IKhachhangRepository khachhangRepository, IKhuyenmaiRepository khuyenmaiRepository, ITourRepository tourRepository, ILoaitourRepository loaitourRepository, IDiemdenRepository hiemdenRepository, IVnPayRepository vnPayRepository, UserManager<User> userManager)
        {
            _phieudattourRepository = phieudattourRepository;
            _khachhangRepository = khachhangRepository;
            _khuyenmaiRepository = khuyenmaiRepository;
            _tourRepository = tourRepository;
            _loaitourRepository = loaitourRepository;
            _hiemdenRepository = hiemdenRepository;
            _vnPayRepository = vnPayRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var phieudattours = await _phieudattourRepository.GetAllAsync();
            var listtour = new List<Tour>();
            foreach(var item in phieudattours)
            {
                var tour = await _tourRepository.GetByIdAsync(item.Matour);
                listtour.Add(tour);
            }
            ViewData["ListTour"] = listtour;
            return View(phieudattours);
        }

        public async Task<IActionResult> Add(int matour)
        {
            var tour = await _tourRepository.GetByIdAsync(matour);
            var user = await _userManager.GetUserAsync(User); // Get current user
            var khachhang = await _khachhangRepository.GetBySdtAsync(user.Sdt);
            var phieudattour = new Phieudattour
            {
                // Set properties for Phieudattour
                Matour = matour,
                Makh = khachhang.Makh, // Assuming Id is used as Makh in Phieudattour
                // other properties as needed
            };
            ViewData["Hoten"] = khachhang.Tenkh;
            ViewData["Tour"] = tour;
            return View(phieudattour);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Phieudattour phieudattour, string payment = "COD")
        {
            var khachhangs = await _khachhangRepository.GetAllAsync();
            var khuyenmais = await _khuyenmaiRepository.GetAllAsync();
            var makh = phieudattour.Makh;
            var matour = phieudattour.Matour;
            var tours = await _tourRepository.GetAllAsync();
            if (!ModelState.IsValid)
            {
                var tour = await _tourRepository.GetByIdAsync(phieudattour.Matour);
                // Thiét lập thời gian khi đặt tour phải nằm trong khoảng thời gian tour đang cho phép 
                var ngaydattour = DateTime.Now.Date;
                var ngaykhoihanh = DateTime.Parse(tour.Ngaykh.ToString());
                if(ngaydattour > ngaykhoihanh)
                {
                    ModelState.AddModelError("ngaydattour","Hiện tại hết thời hạn xảy ra tour này");
                    ViewBag.Khachhangs = new SelectList(khachhangs, "Makh", "Tenkh");
                    //ViewBag.Khuyenmais = new SelectList(khuyenmais, "Makm", "Tenkm");
                    ViewBag.Tours = new SelectList(tours, "Matour", "Tentour");
                    return View(phieudattour);
                }
                phieudattour.Ngaydat = DateOnly.FromDateTime(ngaydattour);
                // thiết lập điều kiện số người trong tour đặt không vượt quá tổng số người có trong 1 tour
                
                if ((tour.Sochodadat + phieudattour.Song) > tour.Soluongtoida || phieudattour.Song == 0)
                {
                    ModelState.AddModelError("Song", "Số người đặt tour này đã đủ hoặc vướt quá giới hạn tối đa của 1 tour");
                    ViewBag.Khachhangs = new SelectList(khachhangs, "Makh", "Tenkh");
                    ViewBag.Tours = new SelectList(tours, "Matour", "Tentour");
                    return View(phieudattour);
                }

                //kiểm tra mã khuyến mãi
                var makm = await _khuyenmaiRepository.GetByIdAsync(phieudattour.Makm);
                var makmzero = await _khuyenmaiRepository.GetByIdAsync("Zero");
                if(makm == null)
                    phieudattour.Makm = makmzero.Makm; 

                await _phieudattourRepository.AddAsync(phieudattour);
                await _phieudattourRepository.UpdateSLTourDaDat(phieudattour.Makh);
                await _phieudattourRepository.UpdateSLTour(phieudattour.Matour);

                if (payment == "VnPay")
                {
                    var vnPayRequest = new VnPaymentRequest
                    {
                        Amount = (double)phieudattour.Tongtien,
                        OrderId = phieudattour.Mapdt,
                        CreatedDate = DateTime.Now
                    };

                    var paymentUrl = _vnPayRepository.CreatePaymentUrl(HttpContext, vnPayRequest);
                    return Redirect(paymentUrl);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.Khachhangs = new SelectList(khachhangs, "Makh", "Tenkh");
            ViewBag.Tours = new SelectList(tours, "Matour", "Tentour");
            return View(phieudattour);
        }

        public async Task<IActionResult> Update(int mapdt)
        {
            var phieudattour = await _phieudattourRepository.GetByIdAsync(mapdt);
            if (phieudattour == null)
            {
                return NotFound();
            }
            var khachhangs = await _khachhangRepository.GetAllAsync();
            var khuyenmais = await _khuyenmaiRepository.GetAllAsync();
            var tours = await _tourRepository.GetAllAsync();
            ViewBag.Khachhangs = new SelectList(khachhangs, "Makh", "Tenkh");
            ViewBag.Khuyenmais = new SelectList(khuyenmais, "Makm", "Tenkm");
            ViewBag.Tours = new SelectList(tours, "Matour", "Tentour");
            return View(phieudattour);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int mapdt, Phieudattour phieudattour)
        {
            var khachhangs = await _khachhangRepository.GetAllAsync();
            var khuyenmais = await _khuyenmaiRepository.GetAllAsync();
            var tours = await _tourRepository.GetAllAsync();
            if (mapdt != phieudattour.Mapdt)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                var tour = await _tourRepository.GetByIdAsync(phieudattour.Matour);
                if(phieudattour.Song > tour.Soluongtoida)
                {
                    ModelState.AddModelError("Song", "Số người đặt tour này đã đủ hoặc vướt quá giới hạn tối đa của 1 tour");
                    ViewBag.Khachhangs = new SelectList(khachhangs, "Makh", "Tenkh");
                    ViewBag.Tours = new SelectList(tours, "Matour", "Tentour");
                    return View(phieudattour);
                }
                //kiểm tra mã khuyến mãi
                var makm = await _khuyenmaiRepository.GetByIdAsync(phieudattour.Makm);
                var makmzero = await _khuyenmaiRepository.GetByIdAsync("Zero");
                if (makm == null)
                    phieudattour.Makm = makmzero.Makm;
                await _phieudattourRepository.UpdateAsync(phieudattour);
                await _phieudattourRepository.UpdateSLTourDaDat(phieudattour.Makh);
                await _phieudattourRepository.UpdateSLTour(phieudattour.Matour);
                return RedirectToAction(nameof(Index));
            }
            ;
            ViewBag.Khachhangs = new SelectList(khachhangs, "Makh", "Tenkh");
            ViewBag.Tours = new SelectList(tours, "Matour", "Tentour");
            return View(phieudattour);
        }

        public async Task<IActionResult> Delete(int mapdt)
        {
            var phieudattour = await _phieudattourRepository.GetByIdAsync(mapdt);
            if (phieudattour == null)
            {
                return NotFound();
            }
            return View(phieudattour);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int mapdt)
        {
            var phieudattour = await _phieudattourRepository.GetByIdAsync(mapdt);
            
            await _phieudattourRepository.DeleteAsync(mapdt);
            await _phieudattourRepository.UpdateSLTourDaDat(phieudattour.Makh);
            await _phieudattourRepository.UpdateSLTour(phieudattour.Matour);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Display(int mapdt)
        {
            var phieudattour = await _phieudattourRepository.GetByIdAsync(mapdt);
            if (phieudattour == null)
            {
                return NotFound();
            }
            return View(phieudattour);
        }
        public async Task<JsonResult> GetTourGia(int matour)
        {
            var tour = await _tourRepository.GetByIdAsync(matour);
            if (tour == null)
            {
                return Json(new { success = false, message = "Tour not found" });
            }
            return Json(new { success = true, gia = tour.Gia, dvt = tour.Dvt });
        }

     


    }
}
