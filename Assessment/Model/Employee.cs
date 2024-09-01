using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment.Model
{
    public class Employee
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int EmployeeID { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public decimal Salary { get; set; }

        [ForeignKey("department")]
        public int departmentId { get; set; }
        public virtual Department department { get; set; }
    }
}
