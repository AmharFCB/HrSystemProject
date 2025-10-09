using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFirstProject.Data;
using MyFirstProject.Filters;
using MyFirstProject.Interfaces;
using MyFirstProject.Models;

namespace MyFirstProject.Controllers
{
    [SessionAuthorize]

    public class EmployeesController : Controller
    {

        private readonly HrDbContext _context;
        private readonly IRepository<Employees> _EmployeesRepository;

        public EmployeesController(HrDbContext context , IRepository<Employees> repository) 
        { 
            _EmployeesRepository = repository;
            _context = context; 
        }


        public IActionResult Index()
        {
            var employees = _context.Employees.Include(e => e.jobs).Include(e => e.city).Include(e => e.EmployeeStatus).ToList();
            return View(employees);
        }
        private void CreateJobs(int selected = 0)
        {
            IEnumerable<Jobs> jobs = _context.Jobs.ToList();
            SelectList selectListItems = new SelectList(jobs, "Id", "JobName", selected);
            ViewBag.JobsList = selectListItems;
        }
        private void CreateCities(int selected = 0)
        {
            IEnumerable<City> cities = _context.cities.ToList();
            SelectList selectListItems = new SelectList(cities, "Id", "CityName", selected);
            ViewBag.CityList = selectListItems;
        }
        private void CreateEmployeeStatus(int selected = 0)
        {
            var statuses = _context.employeeStatuses.ToList();
            ViewBag.empStatusList = new SelectList(statuses, "Id", "StatusName", selected);
        }


        public IActionResult create()
        {
            CreateEmployeeStatus();
            CreateCities();
            CreateJobs();
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
                    CreateEmployeeStatus();
                    CreateCities();
                    CreateJobs();
                    return View(employees);
                }
                _EmployeesRepository.Add(employees);
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
            CreateEmployeeStatus();
            CreateCities();
            CreateJobs();
            var Employees = _EmployeesRepository.GetByUid(Uid);
            return View(Employees);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Employees employees)
        {
            try
            {

                if (!ModelState.IsValid) {
                    CreateEmployeeStatus();
                    CreateCities();
                    CreateJobs();
                    return View(employees); }

                _EmployeesRepository.Update(employees);
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
            var Employees = _EmployeesRepository.GetByUid(Uid);
            return View(Employees);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult PostDelete(string Uid)
        {
            var Employees = _EmployeesRepository.GetByUid(Uid);
            if (Employees != null)
            {
                _EmployeesRepository.Delete(Employees.Id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
    }

