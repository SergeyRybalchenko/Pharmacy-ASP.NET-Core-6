using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index() => View();
    }
}
