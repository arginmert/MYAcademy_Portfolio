using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultAboutComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;
        public _DefaultAboutComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Abouts.FirstOrDefault();
            ViewBag.name = values.NameSurname;
            ViewBag.jobTitle = values.JobTitle;
            ViewBag.description = values.Description;
            ViewBag.openToWork = values.OpenToWork;
            ViewBag.expertise = values.Expert;
            ViewBag.location = values.Location;
            ViewBag.email = values.Email;
            return View();
        }
 
    }
}
