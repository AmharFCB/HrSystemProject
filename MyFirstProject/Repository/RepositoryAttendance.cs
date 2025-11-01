using Microsoft.EntityFrameworkCore;
using MyFirstProject.Data;
using MyFirstProject.Interfaces;
using MyFirstProject.Models;

namespace MyFirstProject.Repository
{
    public class RepositoryAttendance : MainRepository<Attendance>, IRepositoryAttendance
    {
        private readonly HrDbContext _context;
        public RepositoryAttendance(HrDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Attendance> GetAttendanceWithEmployees()
        {
            return _context.Attendances
                .Include(e => e.employees).ToList();
        }
    }
}
