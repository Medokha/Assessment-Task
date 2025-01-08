namespace Assessment.Dtos
{
    public class stagedeptVM
    {
        public long? numberstu { get; set; }
        public long? numberstupaid { get; set; }
        public long? numberstupaidcomplete { get; set; }
        public long? numberstunopaidcomplete { get; set; }
        public decimal? totalmony { get; set; }
        public decimal? monypaid{ get; set; }
        public decimal? monyreduction{ get; set; }
        public decimal? monynopaid{ get; set; }
        public string? stage{ get; set; }
    }
}