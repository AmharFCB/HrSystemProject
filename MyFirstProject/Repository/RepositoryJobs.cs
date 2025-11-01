using Microsoft.EntityFrameworkCore;
using MyFirstProject.Data;
using MyFirstProject.Interfaces;
using MyFirstProject.Models;

namespace MyFirstProject.Repository
{
    public class RepositoryJobs : MainRepository<Jobs>, IRepositoryJobs
    {
        private readonly HrDbContext _context;
        public RepositoryJobs(HrDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Jobs> GetJobsWithDepartment()
        {
            return _context.Jobs.Include(e => e.department).ToList();
        }
    }
}
