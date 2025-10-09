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
    public class AttendanceController : Controller
    {
        private readonly HrDbContext _context;
        private readonly IRepository<Attendance> _AttendanceRepository;

        public AttendanceController(HrDbContext context , IRepository<Attendance> repository) 
        {
            _AttendanceRepository = repository;
            _context = context; 
        }

        public IActionResult Index()
        {
            IEnumerable<Attendance> attendances = _context.Attendances.Include(e => e.employees).ToList();
            return View(attendances);
        }

        private void CreateEmployees(int selected = 0)
        {
            IEnumerable<Employees> employees = _context.Employees.ToList();
            SelectList selectListItems = new SelectList(employees, "Id", "Name", selected);
            ViewBag.EmployeesList = selectListItems;
        }

        public IActionResult create()
        {
           
            CreateEmployees();
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult create(Attendance attendance)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    
                    CreateEmployees();
                    return View(attendance);
                }

                _AttendanceRepository.Add(attendance);
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
           var attendance =  _AttendanceRepository.GetByUid(Uid);
            CreateEmployees();
            return View(attendance);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Attendance attendance)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    
                    CreateEmployees();

                    return View(attendance);
                }
                _AttendanceRepository.Update(attendance);
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

            var attendance = _AttendanceRepository.GetByUid(Uid);
            return View(attendance);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult PostDelete(String Uid)
        {
            var attendance = _AttendanceRepository.GetByUid(Uid);
            if (attendance != null)
            {

                _AttendanceRepository.Delete(attendance.AttendanceId);

            }

            return RedirectToAction(nameof(Index));
        }
    }

    }
