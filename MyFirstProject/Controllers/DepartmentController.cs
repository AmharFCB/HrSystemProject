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

    public class DepartmentController : Controller
    {
        private readonly HrDbContext _context;
        private readonly IRepository<Department> _DepartmentRepository;

        public DepartmentController(HrDbContext context,IRepository<Department> repository)
        { 
            _DepartmentRepository = repository;
            _context = context; 
        }

        public IActionResult Index()
        {
            IEnumerable<Department> departments = _context.Departments.Include(e=>e.employees).ToList();
            return View(departments);
        }
        private void CreateManager(int selected = 0)
        {
            IEnumerable<Employees> employees = _context.Employees.ToList();
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
                _DepartmentRepository.Add(department);
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
            var department = _DepartmentRepository.GetByUid(Uid);
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
                _DepartmentRepository.Update(department);
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
            var department = _DepartmentRepository.GetByUid(Uid);
            return View(department);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult PostDelete(string Uid)
        {
            var department = _DepartmentRepository.GetByUid(Uid);
            if (department != null)
            {
                _DepartmentRepository.Delete(department.Id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
    }
