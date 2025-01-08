using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Assessment.Data.CodeFirstModel
{
    public class Intallments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public int Id { get; set; }

        public string stage { get; set; }

        public DateTime? startstage { get; set; }
        public DateTime? endstage { get; set; }
                       
        public DateTime? startintallmentone { get; set; }
        public DateTime? endintallmentone { get; set; }
        public DateTime? startintallmenttwo { get; set; }
        public DateTime? endintallmenttwo { get; set; }
        public DateTime? startintallmentthree { get; set; }
        public DateTime? endintallmentthree { get; set; }
        public DateTime? startintallmentfour { get; set; }
        public DateTime? endintallmentfour { get; set; }
        public DateTime? startintallmentfive { get; set; }
        public DateTime? endintallmentfive { get; set; }
    }
}
