using Microsoft.AspNetCore.Mvc;
using MyFirstProject.Data;
using MyFirstProject.DTOS;
using MyFirstProject.Models;

namespace MyFirstProject.Controllers
{
    public class AccountController : Controller
    {


        private readonly HrDbContext _context;

        public AccountController(HrDbContext context) { _context = context; }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(EmployeesDto employeesDto)
        {
            if (!ModelState.IsValid)
            {
                return View(employeesDto);
            }

            var employee = _context.Employees
                .FirstOrDefault(e => e.Email == employeesDto.Email && e.Password == employeesDto.Password);

            if (employee != null)
            {

                HttpContext.Session.SetString("Email", employee.Email);
                HttpContext.Session.SetInt32("TypeUser", employee.TypeUser);

                if (employee.TypeUser == 3)
                    return RedirectToAction("Index", "HomeEmployee");
                else
                    return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
