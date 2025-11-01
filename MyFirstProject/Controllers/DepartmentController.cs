using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFirstProject.Data;
using MyFirstProject.Filters;
using MyFirstProject.Interfaces;
using MyFirstProject.Interfaces.IServices;
using MyFirstProject.Models;
using MyFirstProject.Services;

namespace MyFirstProject.Controllers
{
    [SessionAuthorize]

    public class DepartmentController : Controller
    {
        private readonly IDepartmentsServices DepartmentsServices;

        public DepartmentController( IDepartmentsServices departmentsServices)
        {
            DepartmentsServices = departmentsServices;
        }

        public IActionResult Index()
        {
            IEnumerable<Department> departments = DepartmentsServices.GetAll();
            return View(departments);
        }
        private void CreateManager(int selected = 0)
        {
            IEnumerable<Employees> employees = DepartmentsServices.GetEmployees();
            SelectList selectListItems = new SelectList(employees, "Id", "Name", selected);
            ViewBag.EmployeesList = selectListItems;
        }
        
        public IActionResult create()
        {
            CreateManager();
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult create(Department department)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    CreateManager();
                    return View(department);
                }
                DepartmentsServices.Create(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Content("حدث  خطا غير متوقع يرجي الاتصال بالدعم الفني.");
            }
        }

        [HttpGet]
        public IActionResult Edit(string Uid)
        {
            var department = DepartmentsServices.GetByUid(Uid);
            CreateManager();
            return View(department);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Department department)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    CreateManager();
                    return View(department);
                }
                DepartmentsServices.Update(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return Content("حدث  خطا غير متوقع يرجي الاتصال بالدعم الفني.");
                
            }
        }

        [HttpGet]
        public IActionResult Delete(string Uid)
        {
            var department = DepartmentsServices.GetByUid(Uid);
            return View(department);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult PostDelete(string Uid)
        {
            DepartmentsServices.DeleteByUid(Uid);
            return RedirectToAction(nameof(Index));
        }
    }
    }
