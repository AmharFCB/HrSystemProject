using MyFirstProject.Models;

namespace MyFirstProject.Interfaces.IServices
{
    public interface IDepartmentsServices
    {
        IEnumerable<Department> GetAll();
        Department GetByUid(string Uid);
        bool Create(Department department);
        bool Update(Department department);
        bool DeleteByUid(string Uid);

        IEnumerable<Employees> GetEmployees();
    }
}
