using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Divisima.WebUI.Areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class NewscastController : Controller
    {
        IRepository<Newscast> repoNewscast;
        public NewscastController(IRepository<Newscast> _repoNewscast)
        {
            repoNewscast = _repoNewscast;
        }

        [Route("/admin/haberler")]
        public IActionResult Index()
        {
            return View(repoNewscast.GetAll());
        }

        [Route("/admin/haberler/yeni")]
        public IActionResult New()
        {
            return View();
        }

        [Route("/admin/haberler/yeni"), HttpPost]
        public async Task<IActionResult> New(Newscast model)
        {
            if (Request.Form.Files.Any())
            {
                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Newscast"))) Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Newscast"));
                string dosyaAdi = DateTime.Now.Minute + DateTime.Now.Millisecond + Request.Form.Files["Pıcture"].FileName;
                using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Newscast", dosyaAdi), FileMode.Create))//resmin fiziksel kopyası için
                {
                    await Request.Form.Files["Pıcture"].CopyToAsync(stream);
                }
                model.PhotoPath = "/img/Newscast/" + dosyaAdi;//db deki veri için
            }
            await repoNewscast.Add(model);
            return Redirect("/admin/haberler");
        }

        [Route("/admin/slayt/düzenle")]
        public IActionResult Edit(int id)
        {

            return View(repoNewscast.GetBy(x => x.Id == id));
        }

        [Route("/admin/slayt/düzenle"), HttpPost]
        public async Task<IActionResult> Edit(Newscast model)
        {
            if (Request.Form.Files.Any())
            {
                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Newscast"))) Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Newscast"));
                string dosyaAdi = DateTime.Now.Minute + DateTime.Now.Millisecond + Request.Form.Files["Pıcture"].FileName;
                using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Newscast", dosyaAdi), FileMode.Create))//resmin fiziksel kopyası için
                {
                    await Request.Form.Files["Pıcture"].CopyToAsync(stream);
                }
                model.PhotoPath = "/img/Newscast/" + dosyaAdi;//db deki veri için
            }
            await repoNewscast.Update(model);
            return Redirect("/admin/haberler");
        }


        [Route("/admin/haberler/sil"), HttpPost]
        public string Delete(int id)
        {
            try
            {
                Newscast slide = repoNewscast.GetBy(x => x.Id == id) ?? null;
                if (slide != null) repoNewscast.Delete(slide);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
