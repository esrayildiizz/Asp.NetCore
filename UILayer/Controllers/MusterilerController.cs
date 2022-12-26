using Microsoft.AspNetCore.Mvc;

namespace UILayer.Controllers
{
    public class MusterilerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
