namespace Assessment.Data
{
    public class ManagementsInsideInAndOutUserPer
    {
        public long Id { get; set; }


        public int InsideInAndOutId { get; set; }

  
        public int UserId { get; set; }


        public virtual ManagementsInsideInAndOut InsideInAndOut { get; set; } = null!;

        public virtual userpermations User { get; set; } = null!;
    }
}
