using Microsoft.AspNetCore.Mvc;
using MyFirstProject.Interfaces.IServices;

namespace MyFirstProject.Controllers
{
    public class ProfileController : Controller
    {

        public IActionResult Index()
        {



            return View();
        }
    }
}
