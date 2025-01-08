namespace Assessment.Dtos
{
    public class ReceiptsToReturnVM
    {
        public int DSFID { get; set; }
        public long Id { get; set; }
        public string StudentName { get; set; }
        public string Stage { get; set; }
        public string Year { get; set; }
        public string Department { get; set; }
        public string accept { get; set; }
        public long? VoucherNumber { get; set; }
        public DateOnly PaidDate { get; set; }
        public int? UniversitTuition { get; set; }
        public int? Identity { get; set; }
        public int? Penalty { get; set; }
        public int? Training { get; set; }
        public int? Protest { get; set; }
        public int? SupportBook { get; set; }
        public int? Other { get; set; }
        public int? Insurances { get; set; }
        public int? GraduationDoc { get; set; }
        public int? Clearance { get; set; }
        public int? penality { get; set; }
        public int? RecId { get; set; }
    }
}
