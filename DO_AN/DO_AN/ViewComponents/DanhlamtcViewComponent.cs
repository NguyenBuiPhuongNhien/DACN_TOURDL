using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DO_AN.ViewComponents
{
    public class DanhlamtcViewComponent : ViewComponent
    {
        private readonly IDanhlamtcRepository _danhlamtcRepository;

        public DanhlamtcViewComponent(IDanhlamtcRepository danhlamtc)
        {
            _danhlamtcRepository = danhlamtc;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var destinations = await _danhlamtcRepository.GetAllAsync();
            return View(destinations);
        }
    }
}
