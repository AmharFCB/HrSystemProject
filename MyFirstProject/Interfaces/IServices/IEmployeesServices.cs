using MyFirstProject.Models;

namespace MyFirstProject.Interfaces.IServices
{
    public interface IEmployeesServices
    {
        IEnumerable<Employees> GetAll();
        Employees GetByUid(string Uid);
        bool Create(Employees employee);
        bool Update(Employees employee);
        bool DeleteByUid(string Uid);

        IEnumerable<Jobs> GetEmployeesJobs();
        IEnumerable<City> GetCities();
        IEnumerable<EmployeeStatus> GetEmployeeStatuses();

        

    }
}
