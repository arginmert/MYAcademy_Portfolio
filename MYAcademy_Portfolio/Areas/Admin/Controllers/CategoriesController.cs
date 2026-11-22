using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly PortfolioContext _context;
        public CategoriesController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var values = _context.Categories.ToList();

            return View(values);
        }
        public IActionResult DeleteCategory(int id) { 
            var value = _context.Categories.Find(id);
            _context.Categories.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Kategori silindi.";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {

            return View(new Category());
        }
        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var value = _context.Categories.Find(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
