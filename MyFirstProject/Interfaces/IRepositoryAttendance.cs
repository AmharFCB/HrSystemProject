using MyFirstProject.Models;

namespace MyFirstProject.Interfaces
{
    public interface IRepositoryAttendance : IRepository<Attendance>
    {
        IEnumerable<Attendance> GetAttendanceWithEmployees();

    }
}
