using Microsoft.AspNetCore.Mvc;

namespace UILayer.Controllers
{
    public class StartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
