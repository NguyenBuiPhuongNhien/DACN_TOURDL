using DO_AN.Models;
using DO_AN.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DO_AN.ViewComponents
{
    public class DiemthamquanViewComponent : ViewComponent
    {
        private readonly IDiemthamquanRepository diemthamquanRepository;

        public DiemthamquanViewComponent(IDiemthamquanRepository diemthamquan)
        {
            diemthamquanRepository = diemthamquan;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var destinations = await diemthamquanRepository.GetAllAsync();
            return View(destinations);
        }
    }
}
