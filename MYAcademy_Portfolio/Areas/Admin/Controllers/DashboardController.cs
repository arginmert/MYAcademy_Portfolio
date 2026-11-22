using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Models;

namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _env;

        public DashboardController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var now = DateTime.Now;
            var thisMonthStart = new DateTime(now.Year, now.Month, 1);
            var sixMonthsStart = thisMonthStart.AddMonths(-5);
            var tr = new CultureInfo("tr-TR");

            var recentDates = _context.Messages
                .Where(m => m.SendDate >= sixMonthsStart)
                .Select(m => m.SendDate)
                .ToList();

            var monthly = Enumerable.Range(0, 6)
                .Select(i => sixMonthsStart.AddMonths(i))
                .Select(month => new ChartItem
                {
                    Label = month.ToString("MMM", tr),
                    Value = recentDates.Count(d => d.Year == month.Year && d.Month == month.Month)
                })
                .ToList();

            var projectsByCategory = _context.Categories
                .Select(c => new ChartItem { Label = c.CategoryName, Value = c.Projects.Count })
                .OrderByDescending(x => x.Value)
                .ToList();

            var model = new DashboardViewModel
            {
                ProjectCount = _context.Projects.Count(),
                ActiveProjectCount = _context.Projects.Count(p => p.IsActive),
                SkillCount = _context.Skills.Count(),
                ExperienceCount = _context.Experiences.Count(),
                EducationCount = _context.Educations.Count(),
                MessageCount = _context.Messages.Count(),
                UnreadCount = _context.Messages.Count(m => !m.IsRead),
                MessagesThisMonth = _context.Messages.Count(m => m.SendDate >= thisMonthStart),

                LastMessages = _context.Messages
                    .OrderByDescending(m => m.SendDate)
                    .Take(5)
                    .ToList(),

                // Projede tarih alanı olmadığı için en yeni = en büyük Id
                LastProjects = _context.Projects
                    .Include(p => p.Category)
                    .OrderByDescending(p => p.ProjectId)
                    .Take(3)
                    .ToList(),

                ProjectsByCategory = projectsByCategory,
                MonthlyMessages = monthly,

                HasAbout = _context.Abouts.Any(),
                HasCv = System.IO.File.Exists(Path.Combine(_env.WebRootPath, "files", "cv.pdf")),
                ProjectsWithoutGithub = _context.Projects.Count(p => p.GithubUrl == null || p.GithubUrl == ""),
                EmptyCategoryCount = projectsByCategory.Count(x => x.Value == 0)
            };

            return View(model);
        }
    }
}