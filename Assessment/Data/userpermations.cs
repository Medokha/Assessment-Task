using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment.Data
{
    public class userpermations
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string? Username { get; set; }

        [Required]
        public string Role { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? JobTitle { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsWork { get; set; } = true;
        public string? EnName { get; set; }
        public string? MotherName { get; set; }
        public int? NationalIdNumber { get; set; }
        public string? Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Governorate { get; set; }
        public string? Mahalla { get; set; }
        public string? Ziqaq { get; set; }
        public int? HouseNumber { get; set; }
        public string? PersonalPhoto { get; set; }
        public string? NationalId { get; set; }
        public string? DonorCountry { get; set; }
        public string? DonorUniversity { get; set; }
        public DateTime? DateObtainingCertificate { get; set; }
        public string? AcademicUniversityOrder { get; set; }
        public DateTime? HiringDate { get; set; }
        public DateTime? ContractSigningDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string? AppointmentOrder { get; set; }
        public string? Contract { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DateObtainingTitle { get; set; }
        public string? CareerUniversityOrder { get; set; }
        public string? Resignation { get; set; }
        public bool? IsUnemployed { get; set; }
        public int? IdGroup { get; set; }
        public bool? IdPrint { get; set; }


        [ForeignKey("Dep")]
        public int? DepId { get; set; }
        public virtual DepartmentsDepartment Dep { get; set; }

        [ForeignKey("Nationality")]
        public int? NationalityId { get; set; }
        public virtual UseresNationality? Nationality { get; set; }

        [ForeignKey("Certificate")]
        public int? CertificateId { get; set; }
        public virtual UseresCertificate? Certificate { get; set; }

        [ForeignKey("GenField")]
        public int? GenFieldId { get; set; }
        public virtual UseresGenField? GenField { get; set; }

        [ForeignKey("SpifField")]
        public int? SpifFieldId { get; set; }
        public virtual UseresSpifField? SpifField { get; set; }

        [ForeignKey("Type")]
        public int? TypeId { get; set; }
        public virtual UseresType? Type { get; set; }

        [ForeignKey("HireType")]
        public int? HireTypeId { get; set; }
        public virtual UseresHireType? HireType { get; set; }

        [ForeignKey("Titel")]
        public int? TitelId { get; set; }
        public virtual UseresTitel? Titel { get; set; }

        [ForeignKey("PostionType")]
        public int? PostionTypeId { get; set; }
        public virtual UseresPostionType? PostionType { get; set; }

        public virtual ICollection<DepartmentsMaterial> DepartmentsMaterials { get; set; } = new List<DepartmentsMaterial>();
    }
}
