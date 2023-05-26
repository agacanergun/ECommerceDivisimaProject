using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Divisima.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Divisima.WebUI.Controllers
{
    public class CartController : Controller
    {
        IRepository<Product> repoProduct;
        public CartController(IRepository<Product> _repoProduct)
        {
            repoProduct = _repoProduct;
        }

        [Route("/sepetim")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/sepetim/ekle")]
        public string AddCart(int productid, int quantity)
        {
            Product product = repoProduct.GetAll(x => x.ID == productid).Include(x => x.ProductPictures).FirstOrDefault() ?? null;
            if (product != null)//sepete ekleme işlemleri
            {
                Cart cart = new Cart
                {
                    ID = product.ID,
                    Name = product.Name,
                    Picture = product.ProductPictures.Any() ? product.ProductPictures.FirstOrDefault().Picture : "/img/urunHazirlaniyor.png",
                    Price = product.Price,
                    Quantity = quantity
                };
                List<Cart> carts = new List<Cart>();
                bool urunVarmi = false;
                if (Request.Cookies["MyCart"] != null)//daha önce sepete eklenmiş bir ürün varsa
                {
                    carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);

                    foreach (Cart _cart in carts)
                    {
                        if (_cart.ID == productid)
                        {
                            urunVarmi = true;
                            _cart.Quantity += quantity;
                            if (product.Stock < _cart.Quantity) _cart.Quantity = product.Stock;
                            break;
                        }
                    }
                }
                if (urunVarmi == false) carts.Add(cart);
                CookieOptions cookieOptions = new();
                cookieOptions.Expires = DateTime.Now.AddDays(3);
                Response.Cookies.Append("MyCart", JsonConvert.SerializeObject(carts), cookieOptions);
                return product.Name;
            }
            else return "";
        }
    }
}
