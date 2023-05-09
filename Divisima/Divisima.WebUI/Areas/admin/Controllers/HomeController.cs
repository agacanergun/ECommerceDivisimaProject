using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Divisima.WebUI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Divisima.WebUI.Areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class HomeController : Controller
    {
        IRepository<Admin> repoAdmin;
        public HomeController(IRepository<Admin> _repoAdmin)
        {
            repoAdmin = _repoAdmin;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("/admin/Login"), AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [Route("/admin/Login"), HttpPost, AllowAnonymous]
        public IActionResult Login(string Username, string Password, string ReturnUrl)
        {
            string md5Password = GeneralTool.getMD5(Password);
            Admin admin = repoAdmin.GetBy(x => x.UserName == Username && x.Password == md5Password) ?? null;
            if (admin!=null)
            {
                //login
            }
            else
            {
                ViewBag.Error = "Geçersiz Kullanıcı Adı veya Şifre";
            }


            return View();
        }
    }
}
