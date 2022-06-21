using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
