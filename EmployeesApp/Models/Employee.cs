using System.ComponentModel.DataAnnotations;

namespace EmployeesApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [RegularExpression(@"^[a-zA-Zа-яА-Я\s,.'-]+$"), StringLength(100)]
        public string Name { get; set; }
        [RegularExpression(@"^[a-zA-Zа-яА-Я\s,.'-]+$"), StringLength(100)]
        public string Surname { get; set; }
        [RegularExpression(@"^([+]?[0-9\s-\(\)]{3,25})+$")]
        public string Phone { get; set; }
        public int CompanyId { get; set; }

        public virtual Passport Passport { get; set; } = new Passport();
    }
}
