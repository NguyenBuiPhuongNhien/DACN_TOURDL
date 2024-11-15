using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Policy;
using System.Threading.Tasks;

namespace DO_AN.Controllers
{

    public class TourController : Controller
    {
        private readonly ITourRepository _tourRepository;
        private readonly ILichtrinhRepository _lichtrinhRepository;
        private readonly ILoaitourRepository _loaitourRepository;
        private readonly IDiemkhoihanhRepository _diemkhoihanhRepository;
        private readonly IPhieudattourRepository _phieudattourRepository;
        private readonly IDiemdenRepository _diemdenRepository;
        private readonly ICtddRepository _ciemdenRepository;
        private readonly IDanhlamtcRepository _danhlamtcRepository;
        private readonly IDiemthamquanRepository _diemthamquanRepository;


        public TourController(ITourRepository tourRepository, ILichtrinhRepository lichtrinhRepository, ILoaitourRepository loaitourRepository, IDiemkhoihanhRepository diemkhoihanhRepository, IPhieudattourRepository phieudattourRepository, IDiemdenRepository diemdenRepository, ICtddRepository ctddRepository, IDanhlamtcRepository danhlamtcRepository, IDiemthamquanRepository diemthamquanRepository)
        {
            _tourRepository = tourRepository;
            _lichtrinhRepository = lichtrinhRepository;
            _loaitourRepository = loaitourRepository;
            _diemkhoihanhRepository = diemkhoihanhRepository;
            _phieudattourRepository = phieudattourRepository;
            _diemdenRepository = diemdenRepository;
            _ciemdenRepository = ctddRepository;
            _danhlamtcRepository = danhlamtcRepository;
            _diemthamquanRepository = diemthamquanRepository;
        }
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien + "," + SD.Role_Khachhang)]
        public async Task<IActionResult> Index()
        {
            var tours = await _tourRepository.GetAllAsync();
            foreach (var item in tours)
            {
                await _phieudattourRepository.UpdateSLTour(item.Matour);
            }
            return View(tours);
        }
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien)]
        public async Task<IActionResult> IndexAdmin()
        {
            var tours = await _tourRepository.GetAllAsync();
            foreach (var item in tours)
            {
                await _phieudattourRepository.UpdateSLTour(item.Matour);
            }
            return View(tours);
        }
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien + "," + SD.Role_Khachhang)]
        public async Task<IActionResult> ListTour()
        {
            var tours = await _tourRepository.GetAllAsync();
            foreach (var item in tours)
            {
                await _phieudattourRepository.UpdateSLTour(item.Matour);
            }
            return View(tours);
        }
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien + "," + SD.Role_Khachhang)]
        public async Task<IActionResult> IndexLoaiTour(int loaitour)
        {
            var tours = await _tourRepository.GetAllAsync();
            var TourinLoaiTour = new List<Tour>();
            foreach (var item in tours)
            {
                await _phieudattourRepository.UpdateSLTour(item.Matour);
                if (item.Maloai == loaitour)
                {
                    TourinLoaiTour.Add(item);
                }
            }
            return View("ListTour", TourinLoaiTour);
        }
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien + "," + SD.Role_Khachhang)]
        public async Task<IActionResult> IndexDiemDen(int Diemden)
        {
            var tours = await _tourRepository.GetToursByDestinationAsync(Diemden);
            return View("ListTour", tours);
        }
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien)]
        private async Task<string> SaveImage(IFormFile url)
        {
            var savePath = Path.Combine("wwwroot/images", url.FileName); //Thay đổi đường dẫn theo cấu hình của bạn
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await url.CopyToAsync(fileStream);
            }
            return "/images/" + url.FileName; // Trả về đường dẫn tương đối
        }
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien)]
        public async Task<IActionResult> Add()
        {
            var lichtrinhs = await _lichtrinhRepository.GetAllAsync();
            var loaitours = await _loaitourRepository.GetAllAsync();
            var diemkhoihanhs = await _diemkhoihanhRepository.GetAllAsync();
            ViewBag.Lichtrinhs = new SelectList(lichtrinhs, "Malt", "Tenlt");
            ViewBag.Loaitours = new SelectList(loaitours, "Maloai", "Tenloai");
            ViewBag.Diemkhoihanhs = new SelectList(diemkhoihanhs, "Madkh", "Tendkh");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Tour tour, IFormFile Anhdaidien)
        {
            var lichtrinhs = await _lichtrinhRepository.GetAllAsync();
            var loaitours = await _loaitourRepository.GetAllAsync();
            var diemkhoihanhs = await _diemkhoihanhRepository.GetAllAsync();

            //tour.MadkhNavigation = diemkhoihanhs.GetEnumerator(tour.Madkh);

            if (!ModelState.IsValid)
            {

                if (Anhdaidien != null)
                {
                    // Lưu hình ảnh đại diện tham khảo bài 02 hàm SaveImage

                    tour.Anhdaidien = await SaveImage(Anhdaidien);

                }
                try
                {
                    var ngaykh = DateTime.Parse(tour.Ngaykh.ToString());
                    var ngaykt = DateTime.Parse(tour.Ngaykt.ToString());
                    var ngayht = DateTime.Now.Date;
                    //Kiểm tra điều kiện nhập : Ngày khởi hành phải sau ngày hiện tại ít nhất là 10 ngày
                    //DateTime.Parse(tour.Ngaykh.ToString()) chuyển đổi kiểu dữ liệu Dateonly sang Datetime để so sánh
                    //currentDate.AddDays(10) lấy ra ngày hiện tại sau đó cộng thêm 10 ngày để thỏa điều kiện

                    if (ngaykh < ngayht.AddDays(10))
                    {
                        ModelState.AddModelError("Ngaykh", "Nhập sai ngày. Ngày khởi hành phải sau ngày hiện tại ít nhất là 10 ngày.");
                        ViewBag.Lichtrinhs = new SelectList(lichtrinhs, "Malt", "Tenlt");
                        ViewBag.Loaitours = new SelectList(loaitours, "Maloai", "Tenloai");
                        ViewBag.Diemkhoihanhs = new SelectList(diemkhoihanhs, "Madkh", "Tendkh");
                        return View(tour);
                        // Xử lý khi ngày khởi hành của tour cách ít nhất 10 ngày từ ngày hiện tại
                    }

                    // tiếp tục kiểm tra ngày kết thúc có sau ngày khởi hành hay không
                    // Nếu ngày kết thúc trước ngày khởi hành thì vô lý 
                    if (ngaykt < ngaykh || ngaykt == ngaykh)
                    {
                        ModelState.AddModelError("Ngaykt", "Nhập sai ngày. Ngày kết thúc phải  sau khởi hành ít nhất là 1 ngày.");
                        ViewBag.Lichtrinhs = new SelectList(lichtrinhs, "Malt", "Tenlt");
                        ViewBag.Loaitours = new SelectList(loaitours, "Maloai", "Tenloai");
                        ViewBag.Diemkhoihanhs = new SelectList(diemkhoihanhs, "Madkh", "Tendkh");
                        return View(tour);
                    }
                    //Số ngày là khoản cách giữa ngày khởi hành và ngày kết thúc
                    tour.Songay = ngaykt.Day - ngaykh.Day;
                    tour.Sodem = tour.Songay - 1;
                    //Mặc định = 0
                    tour.Sochodadat = 0;
                    await _tourRepository.AddAsync(tour);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("Ngoaile", "Ngoại lệ");
                    ViewBag.Lichtrinhs = new SelectList(lichtrinhs, "Malt", "Tenlt");
                    ViewBag.Loaitours = new SelectList(loaitours, "Maloai", "Tenloai");
                    ViewBag.Diemkhoihanhs = new SelectList(diemkhoihanhs, "Madkh", "Tendkh");
                    return View(tour);
                }
            }

            ViewBag.Lichtrinhs = new SelectList(lichtrinhs, "Malt", "Tenlt");
            ViewBag.Loaitours = new SelectList(loaitours, "Maloai", "Tenloai");
            ViewBag.Diemkhoihanhs = new SelectList(diemkhoihanhs, "Madkh", "Tendkh");
            return View(tour);
        }
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien)]
        public async Task<IActionResult> Update(int matour)
        {
            var tour = await _tourRepository.GetByIdAsync(matour);
            if (tour == null)
            {
                return NotFound();
            }
            var lichtrinhs = await _lichtrinhRepository.GetAllAsync();
            var loaitours = await _loaitourRepository.GetAllAsync();
            var diemkhoihanhs = await _diemkhoihanhRepository.GetAllAsync();
            ViewBag.Lichtrinhs = new SelectList(lichtrinhs, "Malt", "Tenlt");
            ViewBag.Loaitours = new SelectList(loaitours, "Maloai", "Tenloai");
            ViewBag.Diemkhoihanhs = new SelectList(diemkhoihanhs, "Madkh", "Tendkh");
            return View(tour);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int matour, Tour tour, IFormFile Anhdaidien)
        {
            var lichtrinhs = await _lichtrinhRepository.GetAllAsync();
            var loaitours = await _loaitourRepository.GetAllAsync();
            var diemkhoihanhs = await _diemkhoihanhRepository.GetAllAsync();
            if (matour != tour.Matour)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var existingTour = await _tourRepository.GetByIdAsync(matour);

                if (existingTour == null)
                {
                    return NotFound();
                }

                if (Anhdaidien != null)
                {
                    // Lưu hình ảnh mới
                    tour.Anhdaidien = await SaveImage(Anhdaidien);
                }
                else
                {
                    // Giữ nguyên hình ảnh cũ
                    tour.Anhdaidien = existingTour.Anhdaidien;
                }

                try
                {
                    var ngaykh = DateTime.Parse(tour.Ngaykh.ToString());
                    var ngaykt = DateTime.Parse(tour.Ngaykt.ToString());
                    var ngayht = DateTime.Now.Date;
                    //Kiểm tra điều kiện nhập : Ngày khởi hành phải sau ngày hiện tại ít nhất là 10 ngày
                    //DateTime.Parse(tour.Ngaykh.ToString()) chuyển đổi kiểu dữ liệu Dateonly sang Datetime để so sánh
                    //currentDate.AddDays(10) lấy ra ngày hiện tại sau đó cộng thêm 10 ngày để thỏa điều kiện

                    if (ngaykh < ngayht.AddDays(10))
                    {
                        ModelState.AddModelError("Ngaykh", "Nhập sai ngày. Ngày khởi hành phải sau ngày hiện tại ít nhất là 10 ngày.");
                        ViewBag.Lichtrinhs = new SelectList(lichtrinhs, "Malt", "Tenlt");
                        ViewBag.Loaitours = new SelectList(loaitours, "Maloai", "Tenloai");
                        ViewBag.Diemkhoihanhs = new SelectList(diemkhoihanhs, "Madkh", "Tendkh");
                        return View(tour);
                        // Xử lý khi ngày khởi hành của tour cách ít nhất 10 ngày từ ngày hiện tại
                    }

                    // tiếp tục kiểm tra ngày kết thúc có sau ngày khởi hành hay không
                    // Nếu ngày kết thúc trước ngày khởi hành thì vô lý 
                    if (ngaykt < ngaykh || ngaykt == ngaykh)
                    {
                        ModelState.AddModelError("Ngaykt", "Nhập sai ngày. Ngày kết thúc phải  sau khởi hành ít nhất là 1 ngày.");
                        ViewBag.Lichtrinhs = new SelectList(lichtrinhs, "Malt", "Tenlt");
                        ViewBag.Loaitours = new SelectList(loaitours, "Maloai", "Tenloai");
                        ViewBag.Diemkhoihanhs = new SelectList(diemkhoihanhs, "Madkh", "Tendkh");
                        return View(tour);
                    }

                    if (existingTour.Sochodadat > tour.Soluongtoida)
                    {
                        ModelState.AddModelError("Soluongtoida", "Hiện tại số lượng người đặt đang vượt quá số lượng bạn chỉnh sửa!! Hãy thiết lập lại!!");
                        ViewBag.Lichtrinhs = new SelectList(lichtrinhs, "Malt", "Tenlt");
                        ViewBag.Loaitours = new SelectList(loaitours, "Maloai", "Tenloai");
                        ViewBag.Diemkhoihanhs = new SelectList(diemkhoihanhs, "Madkh", "Tendkh");
                        return View(tour);
                    }
                    tour.Songay = ngaykt.Day - ngaykh.Day;
                    tour.Sodem = tour.Songay - 1;
                    tour.Sochodadat = existingTour.Sochodadat;
                    await _phieudattourRepository.UpdateSLTour(matour);
                    await _tourRepository.UpdateAsync(tour);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log lỗi tại đây
                    ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi cập nhật tour.");
                }
            }


            ViewBag.Lichtrinhs = new SelectList(lichtrinhs, "Malt", "Tenlt");
            ViewBag.Loaitours = new SelectList(loaitours, "Maloai", "Tenloai");
            ViewBag.Diemkhoihanhs = new SelectList(diemkhoihanhs, "Madkh", "Tendkh");
            return View(tour);
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien)]
        public async Task<IActionResult> Delete(int matour)
        {
            var tour = await _tourRepository.GetByIdAsync(matour);
            if (tour == null)
            {
                return NotFound();
            }
            return View(tour);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int matour)
        {
            await _tourRepository.DeleteAsync(matour);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien + "," + SD.Role_Khachhang)]
        public async Task<IActionResult> Display(int matour)
        {
            var tour = await _tourRepository.GetByIdAsync(matour);
            if (tour == null)
            {
                return NotFound();
            }
            return View(tour);
        }
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien + "," + SD.Role_Khachhang)]
        public async Task<IActionResult> Detail(int matour)
        {
            var tour = await _tourRepository.GetByIdAsync(matour);
            if (tour == null)
            {
                return NotFound();
            }
            var ctdd = await _ciemdenRepository.GetallMaltAsync(tour.Malt);
            return View(tour);
        }
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_NhanVien + "," + SD.Role_Khachhang)]
        public async Task<IActionResult> Search(string query)
        {
            var tours = await _tourRepository.SearchByNameAsync(query);
            var diemkhoihanh = await _diemkhoihanhRepository.GetAllAsync();
            var diemden = await _diemdenRepository.GetAllAsync();
            var loaitour = await _loaitourRepository.GetAllAsync();
            var danhlamtc = await _danhlamtcRepository.GetAllAsync();
            var diemthamquan = await _diemthamquanRepository.GetAllAsync();
            ViewBag.Diemthamquan = new SelectList(diemthamquan, "Madtq", "Tendtq");
            ViewBag.Diemden = new SelectList(diemden, "Madd", "Tendd");
            ViewBag.Loaitour = new SelectList(loaitour, "Maloai", "Tenloai");
            ViewBag.Diemkhoihanh = new SelectList(diemkhoihanh, "Madkh", "Tendkh");
            ViewBag.Danhlamtc = new SelectList(danhlamtc, "Madl", "Tendl");
            return View("Search", tours);
        }


        [HttpPost]
        public async Task<IActionResult> Timkiem(int madtq, int madd, int maloai, int madkh, int madl)
        {
            //khởi tạo 1 list tour để lưu dữ liệu lọc
            var Listtour = new List<Tour>();
            // tìm kiếm tất cả những tour liên quan đến dữ liệu khách hàng chuyền vào
            var TourInDiemthamquan = await _tourRepository.GetTourInDiemthamquan(madtq);
            var TourInDiemden = await _tourRepository.GetTourInDiemden(madd);
            var TourInLoaitour = await _tourRepository.GetTourInloaitour(maloai);
            var TourInDiemkhoihanh = await _tourRepository.GetTourInDiemkhoihanh(madkh);
            var TourInDanhlam = await _tourRepository.GetTourInDanhlam(madl);

            // Khởi tạo ListTour vào 1 danh sách có dữ liệu
            if(TourInLoaitour.Any())
            {
                Listtour = TourInLoaitour.ToList();
            }

            if (TourInDiemthamquan.Any())
            {
                Listtour = TourInDiemthamquan.ToList();
            }

            if (TourInDiemden.Any())
            {
                Listtour = TourInDiemden.ToList();
            }

            if (TourInDiemkhoihanh.Any())
            {
                Listtour = TourInDiemkhoihanh.ToList();
            }

            if (TourInDanhlam.Any())
            {
                Listtour = TourInDanhlam.ToList();
            }
            // Bắt đầu lọc dữ liệu
            if (TourInLoaitour.Any())
            {
                Listtour = Listtour.Intersect(TourInLoaitour).ToList();
            }

            if (TourInDiemthamquan.Any())
            {
                Listtour = Listtour.Intersect(TourInDiemthamquan).ToList();
            }

            if (TourInDiemden.Any())
            {
                Listtour = Listtour.Intersect(TourInDiemden).ToList();
            }
            if (TourInDiemkhoihanh.Any())
            {
                Listtour = Listtour.Intersect(TourInDiemkhoihanh).ToList();
            }
            if (TourInDanhlam.Any())
            {
                Listtour = Listtour.Intersect(TourInDanhlam).ToList();
            }
            var diemkhoihanh = await _diemkhoihanhRepository.GetAllAsync();
            var diemden = await _diemdenRepository.GetAllAsync();
            var loaitour = await _loaitourRepository.GetAllAsync();
            var danhlamtc = await _danhlamtcRepository.GetAllAsync();
            var diemthamquan = await _diemthamquanRepository.GetAllAsync();
            ViewBag.Diemthamquan = new SelectList(diemthamquan, "Madtq", "Tendtq");
            ViewBag.Diemden = new SelectList(diemden, "Madd", "Tendd");
            ViewBag.Loaitour = new SelectList(loaitour, "Maloai", "Tenloai");
            ViewBag.Diemkhoihanh = new SelectList(diemkhoihanh, "Madkh", "Tendkh");
            ViewBag.Danhlamtc = new SelectList(danhlamtc, "Madl", "Tendl");
            return View("Search",Listtour);
        }

    }
}
