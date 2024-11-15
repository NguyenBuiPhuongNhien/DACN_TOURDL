using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DO_AN.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILoaitourRepository _loaitourRepository;
        private readonly IDiemdenRepository _diemdenRepository;
        private readonly ITourRepository _tourRepository;
        private readonly IPhieudattourRepository _phieudattourRepository;

        public HomeController(
            ILogger<HomeController> logger,
            ILoaitourRepository loaitourRepository,
            IDiemdenRepository diemdenRepository,
            ITourRepository tourRepository,
            IPhieudattourRepository phieudattourRepository
        )
        {
            _logger = logger;
            _loaitourRepository = loaitourRepository;
            _diemdenRepository = diemdenRepository;
            _tourRepository = tourRepository;
            _phieudattourRepository = phieudattourRepository;
        }

        public async Task<IActionResult> Index()
        {

            var tours = await _tourRepository.GetAllAsync();
            foreach (var item in tours)
            {
                await _phieudattourRepository.UpdateSLTour(item.Matour);
                var ratings = item.Danhgia.Select(d => d.Sosao ?? 0);
                var averageRating = ratings.Any() ? ratings.Average() : 0;
                ViewData[$"AverageRating_{item.Matour}"] = averageRating;
            }
            return View(tours);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
