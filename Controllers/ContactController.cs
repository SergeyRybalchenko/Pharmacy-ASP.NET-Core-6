using Microsoft.AspNetCore.Mvc;
using Pharmacy.Domain.Entities;
using Pharmacy.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> SendMessage([Bind("MessageId, FirstName, LastName, Email, Subject, Message")] ContactMessage message)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(message.MessageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(message);
        }

        private bool ProductExists(Guid id)
        {
            return (_context.ContactMessages?.Any(e => e.MessageId == id)).GetValueOrDefault();
        }
    }
}
