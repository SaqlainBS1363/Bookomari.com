using Microsoft.AspNetCore.Mvc;

namespace Bookomari.com.Controllers
{
    public class AuthorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
