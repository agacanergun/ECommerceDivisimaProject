using Microsoft.AspNetCore.Mvc;

namespace Divisima.WebUI.Areas.admin.Controllers
{
    public class InstitutionalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
