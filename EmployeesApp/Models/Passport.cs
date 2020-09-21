using Newtonsoft.Json;

namespace EmployeesApp.Models
{
    public class Passport
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
