using DO_AN.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DO_AN.ViewComponents
{
    public class DiemkhoihanhViewComponent : ViewComponent
    {
        private readonly IDiemkhoihanhRepository _diemkhoihanhRepository;

        public DiemkhoihanhViewComponent(IDiemkhoihanhRepository diemkhoihanhRepository)
        {
            _diemkhoihanhRepository = diemkhoihanhRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var destinations = await _diemkhoihanhRepository.GetAllAsync();
            return View(destinations);
        }
    }
}
