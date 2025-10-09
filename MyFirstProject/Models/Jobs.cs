using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstProject.Models
{
    public class Jobs
    {
        [Key]
        public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();

        public string JobName { get; set; }

        public ICollection<Employees>? employees { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        public Department? department { get; set; }
    }
}
