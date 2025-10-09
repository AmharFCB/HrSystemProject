using System.ComponentModel.DataAnnotations;

namespace MyFirstProject.Models
{
    public class LeaveType
    {

        [Key]
        public int Id { get; set; }

        public string Leavetype { get; set; }

        public ICollection<LeaveRequests> leaveRequests { get; set; }
    }
}
