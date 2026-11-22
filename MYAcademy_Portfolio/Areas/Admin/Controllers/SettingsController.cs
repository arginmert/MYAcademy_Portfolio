using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingsController : Controller
    {
        private readonly PortfolioContext _context;

        public SettingsController(PortfolioContext context)
        {
            _context = context;
        }

        // Listeleme
        public IActionResult Index()
        {
            var setting = _context.Settings.FirstOrDefault();
            return View(setting); 
        }

        
        [HttpGet]
        public IActionResult UpdateSettings()
        {
            var setting = _context.Settings.FirstOrDefault() ?? new Setting();
            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSettings(Setting model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var setting = _context.Settings.FirstOrDefault();
            if (setting == null)
            {
                setting = new Setting();
                _context.Settings.Add(setting);
            }

            setting.SiteTitle = model.SiteTitle.Trim();
            setting.SiteDescription = model.SiteDescription.Trim();
            setting.UpdatedDate = DateTime.Now;
            _context.SaveChanges();

            TempData["Success"] = "Site ayarları güncellendi.";
            return RedirectToAction("Index");
        }
    }
}