using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstProject.Models
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public int NationalID { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password {  get; set; }
        public string? Address { get; set; }
        public DateTime JoiningDate { get; set; }

        [ForeignKey("Jobs")]
        public int? JobsId { get; set; }
        public Jobs? jobs { get; set; }
        [ForeignKey("City")]
        public int? CityId { get; set; }
        public City? city { get; set; }

        [ForeignKey("EmployeeStatus")]
        public int? EmployeeStatusId { get; set; }
        public int TypeUser { get; set; } = 1; // 1 admin 2 hr 3 employee
        public EmployeeStatus? EmployeeStatus { get; set; }

    }
}
