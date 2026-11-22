using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Entities;
using MYAcademy_Portfolio.Models;

namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly PortfolioContext _context;
        public MessageController(PortfolioContext context)
        {
            _context = context;
        }
        public IActionResult MessageList()
        {
            var values = _context.Messages.ToList();
            return View(values);
        }
        public IActionResult CreateMessage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateMessage(Message message)
        {
            message.SendDate = DateTime.Now;
            message.IsRead = false;
            _context.Messages.Add(message);
            _context.SaveChanges();
            return RedirectToAction("MessageList");
        }
        public IActionResult DeleteMessage(int id)
        {
            var value = _context.Messages.Find(id);
            _context.Messages.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("MessageList");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMessage(ContactFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Hataları ve yazılanları forma geri taşı
                TempData["ContactErrors"] = string.Join("|", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                TempData["ContactName"] = model.NameSurname;
                TempData["ContactEmail"] = model.Email;
                TempData["ContactSubject"] = model.Subject;
                TempData["ContactMessage"] = model.MessageDetail;
                return Redirect("/Default/Index/");
            }

            var message = new Message
            {
                NameSurname = model.NameSurname.Trim(),
                Email = model.Email.Trim(),
                Subject = model.Subject.Trim(),
                MessageDetail = model.MessageDetail.Trim(),
                SendDate = DateTime.Now,
                IsRead = false
            };

            _context.Messages.Add(message);
            _context.SaveChanges();

            TempData["ContactSuccess"] = "Mesajınız başarıyla iletildi! Teşekkür ederiz.";
            return Redirect("/Default/Index/");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAsRead(int id)
        {
            var value = _context.Messages.Find(id);
            if (value == null) return NotFound();

            if (!value.IsRead)
            {
                value.IsRead = true;
                _context.SaveChanges();
            }
            return Ok();
        }
    }

}
