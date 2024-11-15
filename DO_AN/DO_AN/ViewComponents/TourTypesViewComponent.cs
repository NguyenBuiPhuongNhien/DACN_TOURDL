using Microsoft.AspNetCore.Mvc;
using DO_AN.Repositories;
using System.Threading.Tasks;

namespace DO_AN.ViewComponents
{
    public class TourTypesViewComponent : ViewComponent
    {
        private readonly ILoaitourRepository _loaitourRepository;

        public TourTypesViewComponent(ILoaitourRepository loaitourRepository)
        {
            _loaitourRepository = loaitourRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loaitours = await _loaitourRepository.GetAllAsync();
            return View(loaitours);
        }
    }
}
