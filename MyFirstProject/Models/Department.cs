using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstProject.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();

        public string DepartmentName { get; set; }
        

        [ForeignKey("Employees")]
        public int? EmployeesId { get; set; }

        public Employees? employees { get; set; }
    }
}
