using System.ComponentModel.DataAnnotations;

namespace MyFirstProject.Models
{
    public class City
    {

        [Key]
        public int Id { get; set; }

        public string CityName { get; set; }

        public ICollection<Employees> employees { get; set; }
    }
}
