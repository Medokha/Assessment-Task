using Assessment.Data.CodeFirstModel;

namespace Assessment.Data
{
    public class Account_waiting_payment
    {

        public int Id { get; set; }


        public DateOnly Date { get; set; }


        public string Notes { get; set; } = null!;



        /// <summary>
        /// TRIAL
        /// </summary>
        public int? Payment { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public int? Identity { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public int? Protest { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public int? SupportBook { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public int? Other { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public int? NonCurrentActivity { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public int? Predecessor { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public int? Deduction { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public int? UseresWaitingId { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public long VoucherNumber { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public int? Support { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public string? IdentityNotes { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public string? OtherNotes { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public string? PaymentNotes { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public string? ProtestNotes { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public string? SupportNotes { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public DateTimeOffset? CreatedDate { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public DateTimeOffset? ModifiedDate { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public int? Insurances { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public string? InsurancesNotes { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public string? SupportBookNotes { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public int? Penalty { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public string? PenaltyNotes { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public int? Training { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public string? TrainingNotes { get; set; }

        /// <summary>
        /// TRIAL
        /// </summary>
        public bool Checked { get; set; }

        public virtual UseresWaiting? UseresWaiting { get; set; }
    }
}
