using Microsoft.AspNetCore.Mvc;
using Pharmacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain;

namespace Pharmacy.Controllers
{
    public class ContactController : Controller
    {
        readonly private ApplicationDBContext _context;

        public ContactController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(ContactMessage message)
        {
            if (ModelState.IsValid)
            {
                message.MessageId = Guid.NewGuid();
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Index();
        }

    }
}
