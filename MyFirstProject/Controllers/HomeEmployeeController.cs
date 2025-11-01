using Microsoft.AspNetCore.Mvc;

namespace MyFirstProject.Controllers
{
    public class HomeEmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
