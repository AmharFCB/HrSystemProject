using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstProject.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }
        public int? EmployeesId { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();

        public Employees? employees { get; set; }
        public DateTime Date {  get; set; }

        public TimeSpan? CheckInTime { get; set; }

        public TimeSpan? CheckOutTime { get; set; }
       

        public string Status
        {
            get
            {
                if (CheckInTime == null)
                    return "Absent";

                if (CheckInTime > new TimeSpan(9, 0, 0)) 
                    return "Late";

                return "Present";
            }
        }

    }
}
