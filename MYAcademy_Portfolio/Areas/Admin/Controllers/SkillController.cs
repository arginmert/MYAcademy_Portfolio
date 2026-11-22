using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillController : Controller
    {
        private readonly PortfolioContext _context;
        public SkillController(PortfolioContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var values = _context.Skills.ToList();
            return View(values);
        }
        public IActionResult DeleteSkill(int id)
        {
            var value = _context.Skills.Find(id);
            _context.Skills.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UpdateSkill(int id)
        {
            var skill = _context.Skills.Find(id);
            if (skill == null) return NotFound();

            ViewBag.Categories = _context.Skills
                .AsEnumerable()
                .GroupBy(s => s.SkillCategoryTitle)
                .Select(g => g.First())
                .OrderBy(s => s.SkillCategoryTitle)
                .ToList();

            return View(skill);
        }
        [HttpPost]
        public IActionResult UpdateSkill(Skill skill)
        {
            _context.Skills.Update(skill);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult CreateSkill()
        {
            ViewBag.Categories = _context.Skills
                .AsEnumerable()
                .GroupBy(s => s.SkillCategoryTitle)
                .Select(g => g.First())
                .OrderBy(s => s.SkillCategoryTitle)
                .ToList();

            return View(new Skill { Level = "5" });
        }
        [HttpPost]
        public IActionResult CreateSkill(Skill skill)
        {
            
            _context.Skills.Add(skill);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
