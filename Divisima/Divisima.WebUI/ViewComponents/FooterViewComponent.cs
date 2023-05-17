using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Divisima.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Divisima.WebUI.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        IRepository<Newscast> repoNewscast;
        IRepository<Institutional> repoInstitutional;
        public FooterViewComponent(IRepository<Newscast> _repoNewscast, IRepository<Institutional> _repoInstitutional)
        {
            repoNewscast = _repoNewscast;
            repoInstitutional = _repoInstitutional;
        }
        public IViewComponentResult Invoke()
        {
            var modelNewscast = repoNewscast.GetAll(x=>true);
            var modelInstitutional = repoInstitutional.GetAll(x=>true);


            FooterVM model = new FooterVM
            {
                Newscasts = modelNewscast,
                Institutional = modelInstitutional
            };
            return View(model);
        }
    }
}
