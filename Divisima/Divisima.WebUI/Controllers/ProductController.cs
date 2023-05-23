using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Divisima.WebUI.Controllers
{
    public class ProductController : Controller
    {
        IRepository<Product> repoProduct;
        public ProductController(IRepository<Product> _repoProduct)
        {
            repoProduct = _repoProduct;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("/detay/{name}-{id}")]
        public IActionResult Details(string name, int id)
        {

            return View(repoProduct.GetAll().Include(x => x.ProductPictures).FirstOrDefault(x => x.ID == id));
        }
    }
}
