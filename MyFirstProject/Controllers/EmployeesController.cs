using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFirstProject.Data;
using MyFirstProject.Filters;
using MyFirstProject.Interfaces;
using MyFirstProject.Interfaces.IServices;
using MyFirstProject.Models;

namespace MyFirstProject.Controllers
{
    [SessionAuthorize]

    public class EmployeesController : Controller
    {

        private readonly IEmployeesServices _EmployeesServices;

        public EmployeesController(IEmployeesServices employeesServices, IUnitOfWork unitOfWork)
        {
           _EmployeesServices = employeesServices;
          
        }


        public IActionResult Index()
        {
            var employees = _EmployeesServices.GetAll();
            return View(employees);
        }
        private void CreateJobs(int selected = 0)
        {
            IEnumerable<Jobs> jobs = _EmployeesServices.GetEmployeesJobs();
            SelectList selectListItems = new SelectList(jobs, "Id", "JobName", selected);
            ViewBag.JobsList = selectListItems;
        }
        private void CreateCities(int selected = 0)
        {
            IEnumerable<City> cities = _EmployeesServices.GetCities();
            SelectList selectListItems = new SelectList(cities, "Id", "CityName", selected);
            ViewBag.CityList = selectListItems;
        }
        private void CreateEmployeeStatus(int selected = 0)
        {
            var statuses = _EmployeesServices.GetEmployeeStatuses();
            ViewBag.empStatusList = new SelectList(statuses, "Id", "StatusName", selected);
        }

        private void LoadDropdownData()
        {
            CreateEmployeeStatus();
            CreateCities();
            CreateJobs();
        }


        public IActionResult create()
        {
            LoadDropdownData();
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult create(Employees employees)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    LoadDropdownData();
                    return View(employees);
                }
                _EmployeesServices.Create(employees);
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
            LoadDropdownData();
            var Employees = _EmployeesServices.GetByUid(Uid);
            return View(Employees);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Employees employees)
        {
            try
            {

                if (!ModelState.IsValid) {
                    LoadDropdownData();
                    return View(employees); }

               _EmployeesServices.Update(employees);
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
            var Employees = _EmployeesServices.GetByUid(Uid);
            return View(Employees);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult PostDelete(string Uid)
        {
            _EmployeesServices.DeleteByUid(Uid);
            return RedirectToAction(nameof(Index));
        }
    }
    }

