
using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Divisima.WebUI.Models;
using Divisima.WebUI.ViewModels;
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
            if (Request.Cookies["MyCart"] != null)
            {
                List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
                if (carts.Count() == 0) return Redirect("/");
                else
                {
                    CartVM cartVM = new CartVM
                    {
                        Carts = carts,
                        Products = repoProduct.GetAll().Include(x => x.ProductPictures).OrderBy(x => Guid.NewGuid()).Take(4)
                    };
                    return View(cartVM);
                }
            }
            else return Redirect("/");
        }

        [Route("/sepetim/sayiver")]
        public int GetCartCount()
        {
            if (Request.Cookies["MyCart"] != null)
            {
                return JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]).Sum(x => x.Quantity);
            }
            else return 0;
        }

        [Route("/sepetim/sil")]
        public string RemoveCart(int productid)
        {
            if (Request.Cookies["MyCart"] != null)
            {
                List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
                carts.Remove(carts.FirstOrDefault(x => x.ID == productid));
                CookieOptions cookieOptions = new();
                cookieOptions.Expires = DateTime.Now.AddDays(3);
                Response.Cookies.Append("MyCart", JsonConvert.SerializeObject(carts), cookieOptions);
                return "OK";
            }
            else return "";
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


        [Route("/sepetim/tamamla")]
        public IActionResult Complete()
        {
            return View();
        }

    }
}
