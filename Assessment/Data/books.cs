using System.ComponentModel.DataAnnotations;

namespace Assessment.Data
{
    public class books
    {
        [Key]
        public int ID { get; set; }
        public string BookType { get; set; }
        public DateTime? Bookdate { get; set; }
        public string BookAddress { get; set; }
        public string StName { get; set; }

        public long STID { get; set; }
        public string BookTypeID { get; set; }
        public string STQSM { get; set; }
        public string STMarhalla { get; set; }
        public string STSType { get; set; }
        public string STSYear { get; set; }
        public string RegUser { get; set; }
        public string? academicYear { get; set; }
        public string? deferYear { get; set; }
        public DateOnly? monaked { get; set; }
                     

    }
}
