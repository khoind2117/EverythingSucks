using Microsoft.AspNetCore.Mvc;

namespace EverythingSucks.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
