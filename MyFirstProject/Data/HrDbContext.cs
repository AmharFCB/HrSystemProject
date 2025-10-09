using Microsoft.EntityFrameworkCore;
using MyFirstProject.Models;

namespace MyFirstProject.Data
{
    public class HrDbContext : DbContext
    {
        public HrDbContext(DbContextOptions<HrDbContext> options) : base(options)
        {
        }

        public DbSet<Employees> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<LeaveRequests> LeaveRequestss { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<City> cities { get; set; }
        public DbSet<LeaveType> leaveTypes { get; set; }
        public DbSet<EmployeeStatus> employeeStatuses { get; set; }
    }
}

