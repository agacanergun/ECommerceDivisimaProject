
using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Divisima.WebUI.Areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class ProductController : Controller
    {
        IRepository<Product> repoProduct;
        IRepository<Brand> repoBrand;
        public ProductController(IRepository<Product> _repoProduct, IRepository<Brand> _repoBrand)
        {
            repoProduct = _repoProduct;
            repoBrand = _repoBrand;
        }

        [Route("/admin/urun")]
        public IActionResult Index()
        {
            return View(repoProduct.GetAll().Include(i => i.Brand).Include(i => i.productCategories).ThenInclude(t=>t.Category));
        }

        [Route("/admin/urun/yeni")]
        public IActionResult New()
        {
            ViewBag.Brands = repoBrand.GetAll().OrderBy(b => b.Name).Select(b => new SelectListItem
            {
                Text = b.Name,
                Value = b.Id.ToString()
            });
            return View();
        }

        [Route("/admin/urun/yeni"), HttpPost]
        public async Task<IActionResult> New(Product model)
        {
            await repoProduct.Add(model);
            return Redirect("/admin/urun");
        }

        [Route("/admin/urun/duzenle")]
        public IActionResult Edit(int id)
        {
            ViewBag.Brands = repoBrand.GetAll().OrderBy(b => b.Name).Select(b => new SelectListItem
            {
                Text = b.Name,
                Value = b.Id.ToString()
            });
            return View(repoProduct.GetBy(x => x.ID == id));
        }

        [Route("/admin/urun/duzenle"), HttpPost]
        public async Task<IActionResult> Edit(Product model)
        {
            await repoProduct.Update(model);
            return Redirect("/admin/urun");
        }

        [Route("/admin/urun/sil"), HttpPost]
        public async Task<string> Delete(int id)
        {
            try
            {
                Product Product = repoProduct.GetBy(x => x.ID == id) ?? null;
                if (Product != null) await repoProduct.Delete(Product);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
