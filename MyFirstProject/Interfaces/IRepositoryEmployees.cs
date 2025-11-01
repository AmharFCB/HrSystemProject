using MyFirstProject.Models;

namespace MyFirstProject.Interfaces
{
    public interface IRepositoryEmployees : IRepository<Employees>
    {
        IEnumerable<Employees> GetEmployeesWithRelations();
        
    }
}
