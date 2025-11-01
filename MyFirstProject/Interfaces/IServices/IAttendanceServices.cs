using MyFirstProject.Models;

namespace MyFirstProject.Interfaces.IServices
{
    public interface IAttendanceServices
    {
        IEnumerable<Attendance> GetAll();
        Attendance GetByUid(string Uid);
        bool Create(Attendance attendance);
        bool Update(Attendance attendance);
        bool DeleteByUid(string Uid);

        IEnumerable<Employees> GetEmployees();

        bool CheckIn(string email);
        bool CheckOut(string email);
        bool HasCheckedIn(string email);
    }
}
