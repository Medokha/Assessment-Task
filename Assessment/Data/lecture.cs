using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment.Data
{
    public class lecture
    {
        public int Id { get; set; }
        public string group { get; set; }
        public string place { get; set; }
        public string edu { get; set; }
        public string year { get; set; }
        public int day { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }

        [ForeignKey("Dep")]
        public int? DepId { get; set; }

        public virtual DepartmentsDepartment Dep { get; set; }

        [ForeignKey("userpermations")]
        public int? userpermationsId { get; set; }

        public virtual userpermations userpermations { get; set; }
    }
}
