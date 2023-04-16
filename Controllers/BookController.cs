using Microsoft.AspNetCore.Mvc;

namespace Bookomari.com.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
