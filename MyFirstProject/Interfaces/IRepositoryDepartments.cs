using MyFirstProject.Models;

namespace MyFirstProject.Interfaces
{
    public interface IRepositoryDepartments : IRepository<Department>
    {
        IEnumerable<Department> GetEmployeesWithDepartment();
    }
}
