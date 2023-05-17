using Divisima.DAL.Entities;

namespace Divisima.WebUI.ViewModels
{
    public class FooterVM
    {
        public IEnumerable<Newscast> Newscasts { get; set; }
        public IEnumerable<Institutional> Institutional { get; set; }
    }
}
