using Microsoft.EntityFrameworkCore;
using MyFirstProject.Data;
using MyFirstProject.Interfaces;
using MyFirstProject.Models;

namespace MyFirstProject.Repository
{
    public class RepositoryEmployees : MainRepository<Employees> , IRepositoryEmployees
    {
        private readonly HrDbContext _context;
        public RepositoryEmployees(HrDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Employees> GetEmployeesWithRelations()
        {
            return _context.Employees
                .Include(e => e.jobs)
                .Include(e => e.city)
                .Include(e => e.EmployeeStatus).ToList();
        }

    }
    
    
}
