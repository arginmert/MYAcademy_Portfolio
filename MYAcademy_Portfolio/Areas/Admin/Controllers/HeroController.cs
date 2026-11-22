using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HeroController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _env;

        public HeroController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var hero = _context.HeroSections.FirstOrDefault() ?? new HeroSection();
            return View(hero);
        }

        [HttpPost]
        public async Task<IActionResult> Index(HeroSection model, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Resim yüklendiyse kaydet
            if (imageFile != null && imageFile.Length > 0)
            {
                var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                var ext = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

                if (!allowed.Contains(ext) || imageFile.Length > 3 * 1024 * 1024)
                {
                    TempData["Error"] = "Resim JPG, PNG veya WEBP olmalı ve 3 MB'ı geçmemeli.";
                    return View(model);
                }

                var folder = Path.Combine(_env.WebRootPath, "images", "hero");
                Directory.CreateDirectory(folder);

                var fileName = Guid.NewGuid() + ext;
                using (var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Eski yerel resmi sil
                if (!string.IsNullOrEmpty(model.ImageUrl) && model.ImageUrl.StartsWith("/images/hero/"))
                {
                    var oldPath = Path.Combine(_env.WebRootPath, model.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                model.ImageUrl = "/images/hero/" + fileName;
            }

            if (model.HeroSectionId == 0)
            {
                _context.HeroSections.Add(model);
            }
            else
            {
                var hero = _context.HeroSections.Find(model.HeroSectionId);
                if (hero == null) return NotFound();

                hero.Title = model.Title;
                hero.SubTitle = model.SubTitle;
                hero.Description = model.Description;
                hero.ImageUrl = model.ImageUrl;
            }

            _context.SaveChanges();
            TempData["Success"] = "Hero bölümü güncellendi.";
            return RedirectToAction("Index");
        }
    }
}