using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;

namespace MYAcademy_Portfolio.ViewComponents.DefaultViewComponents
{
    
    public class _DefaultContactComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultContactComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Contacts.FirstOrDefault();
            ViewBag.desc = values.Description;
            ViewBag.email = values.Email;
            ViewBag.linkedin = values.Linkedin;
            ViewBag.location = values.Location;
            ViewBag.github = values.Github;
            ViewBag.subdesc = values.SubDescription;
            return View();
        }
    }
}
