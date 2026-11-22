using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly PortfolioContext _context;

        public ContactController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var contact = _context.Contacts.FirstOrDefault() ?? new Contact();
            return View(contact);
        }
        [HttpPost]
        public IActionResult Index(Contact contact)
        {
            var values = _context.Contacts.Find(contact.ContactId);
            if (contact == null) return NotFound();

            values.Description = contact.Description;
            values.SubDescription = contact.SubDescription;
            values.Email = contact.Email;
            values.Location = contact.Location;
            values.Linkedin = contact.Linkedin;
            values.Github = contact.Github;
            _context.SaveChanges();
            TempData["Success"] = "İletişim bilgileri kaydedildi.";
            return RedirectToAction("Index");
        }

    }
}
