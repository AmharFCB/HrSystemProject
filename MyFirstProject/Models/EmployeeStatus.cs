namespace MyFirstProject.Models
{
    public class EmployeeStatus
    {
        public int Id { get; set; }

        public string StatusName { get; set; }

        public ICollection<Employees> employees { get; set; }
    }
}
