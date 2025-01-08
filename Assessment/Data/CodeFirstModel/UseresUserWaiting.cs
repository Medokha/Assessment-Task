using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment.Data.CodeFirstModel
{
    public class UseresWaiting
    {
        public long Id { get; set; }


        public string? selectbranch { get; set; }
        public string? selectbranchSpecialization { get; set; }
        public string? tries { get; set; }
        public double? GPA { get; set; }


        public string? competence { get; set; }
        public bool check_payment { get; set; } =false;

        public string? DirectorateName { get; set; }
      

        public string? nationalism { get; set; }

        public int? FrenchGrade { get; set; }
        public string? SchoolDistrict { get; set; }
        public string? LoginWindow { get; set; }
        public string? TypeOfRelatives { get; set; }
        public string? choseestu { get; set; }
        public string? Attendance { get; set; }
        public bool? HasRelatives { get; set; }
        public string? PasswordNumber { get; set; }
        public bool? IsInstituteGraduate { get; set; }
        public bool? IsAmongTopStudents { get; set; }
        public string? InstituteName { get; set; }
        public int? TotalStudentsInBatch { get; set; }
        public string? MinistryUsername { get; set; }
        public string? MinistrySecretCode { get; set; }
        public string? InstituteGraduateYear { get; set; }
        public DateOnly? date_of_issue { get; set; }
        public int? document_count { get; set; }
        public int? document_number { get; set; }
        public string Password { get; set; } = null!;

        public DateTimeOffset? LastLogin { get; set; }

        public bool IsSuperuser { get; set; }

        public string Username { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string FullNameEn { get; set; } = null!;

        public bool IsStaff { get; set; }

        public bool IsActive { get; set; }

        public string? Email { get; set; }

        public string? Univenumber { get; set; }

        public DateOnly? LastCertificateDate { get; set; }

        public DateOnly? BrithDate { get; set; }

        public string? Postion { get; set; }


        public string? Edu { get; set; }
        public string? Eduone { get; set; }
        public string? Edutwo { get; set; }
        public string? Eduthree { get; set; }


        public string? Dorea { get; set; }


        public DateOnly? HiringDate { get; set; }


        public string? MotherName { get; set; }

        public string? CarryName { get; set; }

        public string? CarryRt { get; set; }

        public string? CarryJob { get; set; }


        public string? CarryPhone { get; set; }


        public string? IdNumber { get; set; }


        public string? GenField { get; set; }


        public string? SpifField { get; set; }

        public string? PhoneNumber { get; set; }


        public string? ExamNumbers { get; set; }


        public string? SecectNumbers { get; set; }


        public string? Specfic { get; set; }

        public string? Code { get; set; }

        public int? Rnumber { get; set; }


        public int? GrdNumber { get; set; }


        public string? GrdNumberDate { get; set; }


        public string? RecordNumber { get; set; }


        public DateOnly? RecordNumberDate { get; set; }


        public string? GridNumberDate { get; set; }

        public DateOnly? Date2 { get; set; }


        public string? Gev { get; set; }


        public string? Area { get; set; }


        public string? Store { get; set; }


        public string? Zqaq { get; set; }

        public string? Dar { get; set; }

        public string? Sex { get; set; }


        public double? Totel { get; set; }

        public double? Pres { get; set; }


        public double? UniversyAvg { get; set; }


        public int? Sequence { get; set; }

        public int? GridTotal { get; set; }


        public string? Notes { get; set; }

        public string? SchooName { get; set; }


        public string? MdName { get; set; }

        public string? Role { get; set; }


        public bool IsForword { get; set; }


        public bool IsSigned { get; set; }


        public bool IsWidhdrow { get; set; }


        public string? PersonalPhoto { get; set; }


        public string? IdPhoto { get; set; }


        public string? DadPhoto { get; set; }


        public string? LivePhoto { get; set; }


        public string? SecPhoto { get; set; }


        public string? OuthPhoto { get; set; }

        public string? ApplicationPhoto { get; set; }


        public int? BachelorsId { get; set; }

        public int? MasterId { get; set; }

        public int? NationalityCountryId { get; set; }

        public int? PhDId { get; set; }

        public int? CertificateId { get; set; }

        public string? Channel { get; set; }


        public int? CountryId { get; set; }


        public int? DepId { get; set; }
        public int? DeponeId { get; set; }
        public int? DeptwoId { get; set; }
        public int? DepthreeId { get; set; }


        public int? EndYearId { get; set; }

        public int? HireTypeId { get; set; }


        public int? InationalityId { get; set; }


        public int? NationalityId { get; set; }


        public int? PostionTypeId { get; set; }


        public int? PreviHireId { get; set; }

        public int? ReligionId { get; set; }


        public int? StartYearId { get; set; }

        public int? TitelId { get; set; }

        public int? TypeId { get; set; }


        public int? WayId { get; set; }

        public int? WindowId { get; set; }


        public string? MedicalExamination { get; set; }

        public string? Commitment { get; set; }


        public string? ContractAgreement { get; set; }


        public string? PrepDocument { get; set; }


        public bool SummerTraining { get; set; }

        public string? RegistrationPhoto { get; set; }


        public bool CheckedAndLocked { get; set; }

        public string? EndorsementAddress { get; set; }


        public bool OldAccounts { get; set; }


        public double? FirstInClass { get; set; }
        public string? Roletype { get; set; } = null!;

        public int? AddById { get; set; }


        public string FakePasswordEn { get; set; } = null!;

        public string? MiddleSchoolGraduationYear { get; set; }
        public int? BirthYear { get; set; }


        public string? SuperiorPhoneNumber { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }


        public DateTimeOffset? ModifiedDate { get; set; }


        public string? AppliactionType { get; set; }

        public string? AppliactionNote { get; set; }


        public int? WaitById { get; set; }


        public string? StudentStatus { get; set; }


        public string? Adress { get; set; }

        public long? CitIdNumber { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }


        public string? MotherNameEn { get; set; }


        public string? PlaceOfBrith { get; set; }

        public string? SecondName { get; set; }


        public bool? FilldeForm { get; set; }


        public string? PassportNumber { get; set; }


        public string? MinistryInfo { get; set; }

        public string? UnivPassword { get; set; }


        public DateOnly? Date { get; set; }

        public bool MinistrySigned { get; set; }


        public string? FingerprintLoc { get; set; }

        public int? FingerprintId { get; set; }


        public int? GenFieldId { get; set; }


        public int? SpifFieldId { get; set; }

        public string? SginUpCode { get; set; }


        public string EnFullName { get; set; } = null!;


        public DateOnly? ResignationDate { get; set; }


        public DateOnly? DirectDate { get; set; }


        public bool FingerprintNotReq { get; set; }


        public int? EmptyDays { get; set; }


        public bool? DocumentsCheck { get; set; }


        public TimeOnly? EvningTime { get; set; }

        public TimeOnly? MoringTime { get; set; }


        public string? UniversyAvgInWords { get; set; }


        public string? FirstInClassUniversyAvgInWords { get; set; }

        public DateOnly? GridOrderDate { get; set; }

        public string? GridOrderNumber { get; set; }

        public string? AssociationReceiptFile { get; set; }

        public string? AssociationReceiptRecord { get; set; }


        public string? MainStatus { get; set; }

        public string? Contract { get; set; }

        public string? Cv { get; set; }


        public string? UniversyCirtifat { get; set; }


        public string? UniversyOrder { get; set; }


        public string? Others { get; set; }

        public string? PersonalFile { get; set; }

        public string? WorkOrder { get; set; }


        public string? TitleOrder { get; set; }


        public DateOnly? CitIdNumberDate { get; set; }


        public DateOnly? CitIdNumberEndDate { get; set; }

        public int? FrinshTotel { get; set; }


        public bool IsDors { get; set; }


        public int? NumberOfClass { get; set; }
        public int createdbyid { get; set; }
        public virtual userpermations? createdby { get; set; }

        public int? editbyid { get; set; }
        public virtual UseresUserUserPermission? UserUserPermission { get; set; }

        public string? Passport { get; set; }


        public DateOnly? PassportNumberDate { get; set; }

        public DateOnly? PassportNumberEndDate { get; set; }

        public string? Trial479 { get; set; }
        public virtual DepartmentsDepartment? Dep { get; set; }
        public virtual DepartmentsDepartment? Depone { get; set; }
        public virtual DepartmentsDepartment? Deptwo { get; set; }
        public virtual DepartmentsDepartment? Depthree { get; set; }
        public virtual UseresNationality? Nationality { get; set; }
        public virtual UseresReligion? Religion { get; set; }
        public virtual DepartmentsYear? StartYear { get; set; }
        public virtual UseresWay? Way { get; set; }

        public virtual UseresWindow? Window { get; set; }
        public string? BloodTest { get; set; }
        public string? InstituteDocument { get; set; }
        public string? EndorsementOfEarlyBatchSequences { get; set; }
        public string? DrugExamination { get; set; }
    }

}