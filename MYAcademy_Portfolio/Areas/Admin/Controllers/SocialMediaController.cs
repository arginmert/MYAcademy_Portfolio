using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialMediaController : Controller
    {
        private readonly PortfolioContext _context;

        public SocialMediaController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var values = _context.SocialMedias.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSocialMedia()
        {
            return View(new SocialMedia { IsActive = true });
        }

        [HttpPost]
        public IActionResult CreateSocialMedia(SocialMedia model)
        {
            if (string.IsNullOrWhiteSpace(model.MediaIcon))
                ModelState.AddModelError("MediaIcon", "Lütfen bir ikon seçin.");

            if (!ModelState.IsValid)
                return View(model);

            if (!string.IsNullOrWhiteSpace(model.MediaUrl) &&
                !model.MediaUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase) &&
                !model.MediaUrl.StartsWith("mailto:", StringComparison.OrdinalIgnoreCase))
            {
                model.MediaUrl = "https://" + model.MediaUrl.Trim();
            }

            _context.SocialMedias.Add(model);
            _context.SaveChanges();
            TempData["Success"] = "Sosyal medya bağlantısı eklendi.";
            return RedirectToAction("Index");
        }

        // Aktif ↔ pasif
        public IActionResult ToggleSocialMedia(int id)
        {
            var value = _context.SocialMedias.Find(id);
            if (value != null)
            {
                value.IsActive = !value.IsActive;
                _context.SaveChanges();
                TempData["Success"] = value.IsActive ? "Bağlantı aktif edildi." : "Bağlantı pasif yapıldı.";
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteSocialMedia(int id)
        {
            var value = _context.SocialMedias.Find(id);
            if (value != null)
            {
                _context.SocialMedias.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Bağlantı silindi.";
            }
            return RedirectToAction("Index");
        }
    }
}