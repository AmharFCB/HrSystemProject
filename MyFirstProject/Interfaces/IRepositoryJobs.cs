using MyFirstProject.Models;

namespace MyFirstProject.Interfaces
{
    public interface IRepositoryJobs : IRepository<Jobs>
    {
        IEnumerable<Jobs> GetJobsWithDepartment();
    }
}
