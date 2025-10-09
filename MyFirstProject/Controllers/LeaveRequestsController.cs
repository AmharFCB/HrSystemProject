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

    public class LeaveRequestsController : Controller
    {
        private readonly HrDbContext _context;
        private readonly IRepository<LeaveRequests> _LeaveRequestsRepository;

        public LeaveRequestsController(HrDbContext context,IRepository<LeaveRequests> repository)
        { 
            _LeaveRequestsRepository = repository;
            _context = context; 
        }
        public IActionResult Index()
        {

            IEnumerable<LeaveRequests> leaveRequests = _context.LeaveRequestss.Include(e => e.employees).Include(e => e.leaveType).ToList();
            //IEnumerable<LeaveRequests> LeaveRequests = _context.LeaveRequestss.Include(e => e.leaveType).ToList();
            return View(leaveRequests);
        }
        private void CreateEmployees(int selected = 0)
        {
            IEnumerable<Employees> employees = _context.Employees.ToList();
            SelectList selectListItems = new SelectList(employees, "Id", "Name", selected);
            ViewBag.EmployeesList = selectListItems;
        }

        private void CreateleaveType(int selected = 0)
        {
            IEnumerable<LeaveType> leaveTypes = _context.leaveTypes.ToList();
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
        public IActionResult create(LeaveRequests leaveRequests)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    CreateleaveType();
                    CreateEmployees();
                    return View(leaveRequests);
                }
                _LeaveRequestsRepository.Add(leaveRequests);
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
            var leaveRequestss = _LeaveRequestsRepository.GetByUid(Uid);
            CreateEmployees();
            CreateleaveType();
            return View(leaveRequestss);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(LeaveRequests leaveRequests)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    CreateleaveType();
                    CreateEmployees();

                    return View(leaveRequests);
                }
                _LeaveRequestsRepository.Update(leaveRequests);
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

            var leaveRequestss = _LeaveRequestsRepository.GetByUid(Uid);
            return View(leaveRequestss);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult PostDelete(string Uid)
        {
            var leaveReq = _LeaveRequestsRepository.GetByUid(Uid);
            if (leaveReq != null)
            {
                _LeaveRequestsRepository.Delete(leaveReq.LeaveID);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

