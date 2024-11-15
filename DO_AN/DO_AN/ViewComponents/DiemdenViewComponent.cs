using DO_AN.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace DO_AN.ViewComponents
{
    public class DiemdenViewComponent : ViewComponent
    {
        private readonly IDiemdenRepository _diemdenRepository;

        public DiemdenViewComponent(IDiemdenRepository diemdenRepository)
        {
            _diemdenRepository = diemdenRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var destinations = await _diemdenRepository.GetAllAsync();
            return View(destinations);
        }
    }
}
