using Microsoft.EntityFrameworkCore;
using MyFirstProject.Data;
using MyFirstProject.Interfaces;
using MyFirstProject.Models;

namespace MyFirstProject.Repository
{
    public class RepositoryDepartments : MainRepository<Department>, IRepositoryDepartments
    {
        private readonly HrDbContext _context;
        public RepositoryDepartments(HrDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetEmployeesWithDepartment()
        {
            return _context.Departments.Include(e => e.employees).ToList();
        }
    }
}
