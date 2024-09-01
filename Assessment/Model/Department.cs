
using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment.Model
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    }
}
