using Microsoft.AspNetCore.Mvc;

namespace EverythingSucks.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
