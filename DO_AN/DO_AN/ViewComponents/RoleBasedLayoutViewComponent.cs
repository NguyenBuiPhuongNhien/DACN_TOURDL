using DO_AN.Models;
using Microsoft.AspNetCore.Mvc;

public class RoleBasedLayoutViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var user = HttpContext.User;
        if (user.IsInRole(SD.Role_Admin))
        {
            return View("~/Areas/Admin/Views/Shared/_LayoutAdmin.cshtml");
        }
        // Nếu user không thuộc role Admin, trả về layout mặc định
        return View("~/Views/Shared/_Layout.cshtml");
    }
}