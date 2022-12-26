using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "API Running....";
        }
    }
}
