using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Divisima.WebUI.Areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class SlideController : Controller
    {
        IRepository<Slide> repoSlide;
        public SlideController(IRepository<Slide> _repoSlide)
        {
            repoSlide = _repoSlide;
        }



        [Route("/admin/slayt")]
        public IActionResult Index()
        {
            return View(repoSlide.GetAll());
        }

        [Route("/admin/slayt/yeni")]
        public IActionResult New()
        {
            return View();
        }
        [Route("/admin/slayt/yeni"),HttpPost]
        public IActionResult New(Slide model)
        {
            repoSlide.Add(model);
            return Redirect("/admin/slayt");
        }


    }
}
