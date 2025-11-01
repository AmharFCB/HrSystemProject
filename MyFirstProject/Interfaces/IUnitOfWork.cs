using MyFirstProject.Models;

namespace MyFirstProject.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryAttendance RepositoryAttendance { get; }
        IRepositoryDepartments RepositoryDepartments { get; }
        IRepositoryEmployees RepositoryEmployees { get; }
        IRepositoryJobs RepositoryJobs { get; }
        IRepositoryLeaveRequests RepositoryLeaveRequests { get; }

        IRepository<City> Cities { get; }
        IRepository<LeaveType> LeaveTypes { get; }
        IRepository<LeaveRequests> LeaveRequests { get; }
        IRepository<Jobs> Job { get; }
        IRepository<EmployeeStatus> EmployeeStatuses { get; }
        IRepository<Employees> Employeess { get; }

        void SaveChanges();
    }
}
