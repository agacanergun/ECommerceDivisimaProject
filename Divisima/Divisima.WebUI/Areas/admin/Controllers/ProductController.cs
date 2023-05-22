
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
        public ProductController(IRepository<Product> _repoProduct)
        {
            repoProduct = _repoProduct;
        }

        [Route("/admin/urun")]
        public IActionResult Index()
        {
            return View(repoProduct.GetAll());
        }

        [Route("/admin/ürün/yeni")]
        public IActionResult New()
        {
            return View();
        }

        [Route("/admin/ürün/yeni"), HttpPost]
        public async Task<IActionResult> New(Product model)
        {
            await repoProduct.Add(model);
            return Redirect("/admin/ürün");
        }

        [Route("/admin/ürün/duzenle")]
        public IActionResult Edit(int id)
        {
            return View(repoProduct.GetBy(x => x.ID == id));
        }

        [Route("/admin/ürün/duzenle"), HttpPost]
        public async Task<IActionResult> Edit(Product model)
        {
            await repoProduct.Update(model);
            return Redirect("/admin/ürün");
        }

        [Route("/admin/ürün/sil"), HttpPost]
        public string Delete(int id)
        {
            try
            {
                Product Product = repoProduct.GetBy(x => x.ID == id) ?? null;
                if (Product != null) repoProduct.Delete(Product);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
