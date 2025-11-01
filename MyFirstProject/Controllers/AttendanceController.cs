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
    public class AttendanceController : Controller
    {
        private readonly IAttendanceServices AttendanceServices;

        public AttendanceController(IAttendanceServices attendanceServices)
        {
            AttendanceServices = attendanceServices;
        }



        public IActionResult Index()
        {
            IEnumerable<Attendance> attendances = AttendanceServices.GetAll();
            return View(attendances);
        }

        private void CreateEmployees(int selected = 0)
        {
            IEnumerable<Employees> employees = AttendanceServices.GetEmployees();
            SelectList selectListItems = new SelectList(employees, "Id", "Name", selected);
            ViewBag.EmployeesList = selectListItems;
        }

        //public IActionResult create()
        //{

        //    CreateEmployees();
        //    return View();
        //}

        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public IActionResult create(Attendance attendance)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {

        //            CreateEmployees();
        //            return View(attendance);
        //        }

        //        AttendanceServices.Create(attendance);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return Content("حدث  خطا غير متوقع يرجي الاتصال بالدعم الفني.");
        //    }
        //}


        [HttpGet]
        public IActionResult Edit(string Uid)
        {
            var attendance = AttendanceServices.GetByUid(Uid);
            CreateEmployees();
            return View(attendance);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Attendance attendance)
        {

            if (!ModelState.IsValid)
            {
                CreateEmployees();

                return View(attendance);
            }
            AttendanceServices.Update(attendance);
            return RedirectToAction(nameof(Index));
        }


        
        //[HttpGet]
        //public IActionResult Delete(string Uid)
        //{

        //    var attendance = AttendanceServices.GetByUid(Uid);
        //    return View(attendance);
        //}
        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public IActionResult PostDelete(String Uid)
        //{
        //    AttendanceServices.DeleteByUid(Uid);
        //    return RedirectToAction(nameof(Index));
        //}

        public IActionResult CheckIn()
        {
            // Get the email of the logged-in user from the session
            var email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            // Call the CheckIn method from the AttendanceServices
            AttendanceServices.CheckIn(email);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult CheckOut()
        {
            
            var email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }
            
            AttendanceServices.CheckOut(email);
            return RedirectToAction("Index", "Attendance");
        }

    }
}
