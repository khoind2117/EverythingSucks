using Microsoft.AspNetCore.Mvc;

namespace EverythingSucks.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
