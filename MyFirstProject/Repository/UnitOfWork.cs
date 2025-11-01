using MyFirstProject.Data;
using MyFirstProject.Interfaces;
using MyFirstProject.Models;

namespace MyFirstProject.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HrDbContext _context;
        public UnitOfWork(HrDbContext context)
        {
            _context = context;
            RepositoryAttendance = new RepositoryAttendance(_context);
            RepositoryDepartments = new RepositoryDepartments(_context);
            RepositoryEmployees = new RepositoryEmployees(_context);
            RepositoryJobs = new RepositoryJobs(_context);
            RepositoryLeaveRequests = new RepositoryLeaveRequests(_context);
            Cities = new MainRepository<City>(_context);
            LeaveTypes = new MainRepository<LeaveType>(_context);
            EmployeeStatuses = new MainRepository<EmployeeStatus>(_context);
            Employeess = new MainRepository<Employees>(_context);
            Job = new MainRepository<Jobs>(_context);
            LeaveRequests = new MainRepository<LeaveRequests>(_context);
        }



        public IRepositoryAttendance RepositoryAttendance { get; }

        public IRepositoryDepartments RepositoryDepartments { get; }

        public IRepositoryEmployees RepositoryEmployees { get; }

        public IRepositoryJobs RepositoryJobs { get; }

        public IRepositoryLeaveRequests RepositoryLeaveRequests { get; }

        public IRepository<City> Cities { get; }

        public IRepository<LeaveType> LeaveTypes { get; }

        public IRepository<EmployeeStatus> EmployeeStatuses { get; }

        public IRepository<Employees> Employeess { get; }
        public IRepository<Jobs> Job { get; }
        public IRepository<LeaveRequests> LeaveRequests { get; }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }

  
    }
