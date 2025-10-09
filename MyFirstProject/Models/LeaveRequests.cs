using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstProject.Models
{
    public class LeaveRequests
    {
        [Key]
        public int LeaveID { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("Employees")]
        public int? EmployeesId { get; set; }

        public Employees? employees { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [ForeignKey("LeaveType")]
        public int? LeaveTypeId { get; set; }
        public LeaveType? leaveType { get; set; }
        public int LeaveDays { get; set; }
        public string Status { get; set; }

    }
}
