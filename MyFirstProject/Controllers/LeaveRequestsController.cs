using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFirstProject.Data;
using MyFirstProject.Filters;
using MyFirstProject.Interfaces;
using MyFirstProject.Interfaces.IServices;
using MyFirstProject.Models;
using MyFirstProject.Repository;

namespace MyFirstProject.Controllers
{
    [SessionAuthorize]

    public class LeaveRequestsController : Controller
    {
        private readonly ILeaveRequestsServices _leaveRequestsServices;


        public LeaveRequestsController(ILeaveRequestsServices leaveRequestsServices)
        { 
            _leaveRequestsServices = leaveRequestsServices;
        }
        public IActionResult Index()
        {

            IEnumerable<LeaveRequests> leaveRequests = _leaveRequestsServices.GetAll();

            return View(leaveRequests);
        }
        private void CreateEmployees(int selected = 0)
        {
            IEnumerable<Employees> employees = _leaveRequestsServices.GetEmployees();
            SelectList selectListItems = new SelectList(employees, "Id", "Name", selected);
            ViewBag.EmployeesList = selectListItems;
        }

        private void CreateleaveType(int selected = 0)
        {
            IEnumerable<LeaveType> leaveTypes = _leaveRequestsServices.GetLeaveTypes();
            SelectList selectListItems = new SelectList(leaveTypes, "Id", "Leavetype", selected);
            ViewBag.leaveTypeList = selectListItems;
        }

        public IActionResult create()
        {
            CreateleaveType();
            CreateEmployees();
            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(LeaveRequests leaveRequests)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    CreateleaveType();
                    CreateEmployees();
                    return View(leaveRequests);
                }

                await _leaveRequestsServices.CreateLeaveRequestAsync(leaveRequests);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Content("حدث خطأ غير متوقع، يرجى الاتصال بالدعم الفني.");
            }
        }

        [HttpGet]
        public IActionResult Edit(string Uid)
        {
            var leaveRequestss =  _leaveRequestsServices.GetByUid(Uid);
            CreateEmployees();
            CreateleaveType();
            return View(leaveRequestss);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(LeaveRequests leaveRequests)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    CreateleaveType();
                    CreateEmployees();
                    return View(leaveRequests);
                }

                await _leaveRequestsServices.UpdateAsync(leaveRequests);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Content("حدث خطأ غير متوقع، يرجى الاتصال بالدعم الفني.");
            }
        }

        [HttpGet]
        public IActionResult Delete(string Uid)
        {

            var leaveRequestss = _leaveRequestsServices.GetByUid(Uid);
            return View(leaveRequestss);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult PostDelete(string Uid)
        {
            _leaveRequestsServices.DeleteByUid(Uid);
            return RedirectToAction(nameof(Index));

        }
    }
}

