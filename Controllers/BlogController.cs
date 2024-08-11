using Microsoft.AspNetCore.Mvc;

namespace EverythingSucks.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
