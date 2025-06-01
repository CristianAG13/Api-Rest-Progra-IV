using Microsoft.AspNetCore.Mvc;

namespace WebApiProyect
{
    public class AuthsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
