using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;





namespace Divisima.WebUI.Areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class InstitutionalController : Controller
    {
        IRepository<Institutional> repoInstitutional;
        public InstitutionalController(IRepository<Institutional> _repoInstitutional)
        {
            repoInstitutional = _repoInstitutional;
        }

        [Route("/admin/kurumsal")]
        public IActionResult Index()
        {
            return View(repoInstitutional.GetAll());
        }

        [Route("/admin/kurumsal/yeni")]
        public IActionResult New()
        {
            return View();
        }

        [Route("/admin/kurumsal/yeni"), HttpPost]
        public async Task<IActionResult> New(Institutional model)
        {
            await repoInstitutional.Add(model);
            return Redirect("/admin/kurumsal");
        }

        [Route("/admin/kurumsal/duzenle")]
        public IActionResult Edit(int id)
        {
            return View(repoInstitutional.GetBy(x => x.Id == id));
        }

        [Route("/admin/kurumsal/duzenle"), HttpPost]
        public async Task<IActionResult> Edit(Institutional model)
        {
            await repoInstitutional.Update(model);
            return Redirect("/admin/kurumsal");
        }

        [Route("/admin/kurumsal/sil"), HttpPost]
        public string Delete(int id)
        {
            try
            {
                Institutional brand = repoInstitutional.GetBy(x => x.Id == id) ?? null;
                if (brand != null) repoInstitutional.Delete(brand);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
