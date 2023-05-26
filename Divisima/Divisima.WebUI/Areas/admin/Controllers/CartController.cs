using Microsoft.AspNetCore.Mvc;

namespace Divisima.WebUI.Areas.admin.Controllers
{
    public class CartController : Controller
    {
        [Route("/sepetim")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
