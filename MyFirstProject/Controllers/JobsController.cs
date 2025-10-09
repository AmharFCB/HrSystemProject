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

    public class JobsController : Controller
    {

        private readonly HrDbContext _context;
        private readonly IRepository<Jobs> _JobsRepository;

        public JobsController(HrDbContext context , IRepository<Jobs> repository)
        { _JobsRepository = repository;
            _context = context; }

        public IActionResult Index()
        {
            IEnumerable<Jobs> jobs = _context.Jobs.Include(e=>e.department).ToList();
            return View(jobs);
        }
        private void Createdepartment(int selected = 0)
        {
            IEnumerable<Department> departments = _context.Departments.ToList();
            SelectList selectListItems = new SelectList(departments, "Id", "DepartmentName", selected);
            ViewBag.departmentList = selectListItems;
        }
        public IActionResult create()
        {
            Createdepartment();
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult create(Jobs jobs)
        {
            try
            {
                if (!ModelState.IsValid) {
                    Createdepartment();
                    return View(jobs);
                }

                _JobsRepository.Add(jobs);
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
            var Jobs = _JobsRepository.GetByUid(Uid);
            Createdepartment();
            return View(Jobs);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Jobs jobs)
        {
            try
            {
                if (!ModelState.IsValid) {
                    Createdepartment();
                    return View(jobs);
                }
                _JobsRepository.Update(jobs);
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
            var Jobs = _JobsRepository.GetByUid(Uid);
            return View(Jobs);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult PostDelete(string Uid)
        {
            var Jobs = _JobsRepository.GetByUid(Uid);
            if (Jobs != null)
            {
                _JobsRepository.Delete(Jobs.Id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

