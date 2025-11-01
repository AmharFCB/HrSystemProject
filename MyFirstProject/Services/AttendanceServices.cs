using MyFirstProject.Interfaces;
using MyFirstProject.Interfaces.IServices;
using MyFirstProject.Models;

namespace MyFirstProject.Services
{
    public class AttendanceServices : IAttendanceServices
    {

        private readonly IUnitOfWork _unitOfWork;

        public AttendanceServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Create(Attendance attendance)
        {


            _unitOfWork.RepositoryAttendance.Add(attendance);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteByUid(string Uid)
        {
            var Attendance = _unitOfWork.RepositoryAttendance.GetByUid(Uid);
            if (Attendance == null)
            {
                return false;
            }
            _unitOfWork.RepositoryAttendance.Delete(Attendance.AttendanceId);
            _unitOfWork.SaveChanges();
            return true;
        }

        public IEnumerable<Attendance> GetAll()
        {
            return _unitOfWork.RepositoryAttendance.GetAttendanceWithEmployees();

        }

        public Attendance GetByUid(string Uid)
        {
            return _unitOfWork.RepositoryAttendance.GetByUid(Uid);
        }

        public IEnumerable<Employees> GetEmployees()
        {
            return _unitOfWork.Employeess.GetAll();
        }

        public bool Update(Attendance attendance)
        {
            var existing = _unitOfWork.RepositoryAttendance.GetByUid(attendance.Uid);
            if (existing == null)
                return false;

            existing.CheckInTime = attendance.CheckInTime;
            existing.CheckOutTime = attendance.CheckOutTime;

            _unitOfWork.RepositoryAttendance.Update(existing);
            _unitOfWork.SaveChanges();
            return true;
        }


        public bool CheckIn(string email)
        {

            // ابحث في جدول الموظفين عن الموظف اللي إيميله هو نفس الإيميل اللي أرسلناه 
            var employee = _unitOfWork.Employeess.GetAll().FirstOrDefault(e => e.Email == email);

            // لو الموظف مش موجود رجع false
            if (employee == null)
                return false;
            // خزن تاريخ اليوم في متغير
            var today = DateTime.Today;

            // ابحث في جدول الحضور عن سجل الحضور الخاص بالموظف في تاريخ اليوم
            var attendance = _unitOfWork.RepositoryAttendance
                .GetAttendanceWithEmployees()
                .FirstOrDefault(a => a.EmployeesId == employee.Id && a.Date.Date == today);
            // لو سجل الحضور موجود رجع true
            if (attendance != null)
                return true;

            // لو سجل الحضور مش موجود، اعمل سجل جديد
            var newAttendance = new Attendance
            {
                EmployeesId = employee.Id,
                Date = today,
                CheckInTime = DateTime.Now.TimeOfDay
            };

            // أضف سجل الحضور الجديد في قاعدة البيانات
            _unitOfWork.RepositoryAttendance.Add(newAttendance);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool CheckOut(string email)
        {
            var employee = _unitOfWork.Employeess.GetAll().FirstOrDefault(e => e.Email == email);
            if (employee == null)
                return false;

            var today = DateTime.Today;
            var attendance = _unitOfWork.RepositoryAttendance
                .GetAttendanceWithEmployees()
                .FirstOrDefault(a => a.EmployeesId == employee.Id && a.Date.Date == today);

            if (attendance == null || attendance.CheckOutTime != null)
                return false;
            attendance.CheckOutTime = DateTime.Now.TimeOfDay;
            _unitOfWork.RepositoryAttendance.Update(attendance);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool HasCheckedIn(string email)
        {
            
            var employee = _unitOfWork.Employeess.GetAll().FirstOrDefault(e => e.Email == email);
            if (employee == null)
                return false;
            var today = DateTime.Today;

            // ابحث في جدول الحضور عن سجل الحضور الخاص بالموظف في تاريخ اليوم
            var attendance = _unitOfWork.RepositoryAttendance
                .GetAttendanceWithEmployees()
                .FirstOrDefault(a => a.EmployeesId == employee.Id && a.Date.Date == today);
            return attendance != null;
        }

    }
}
