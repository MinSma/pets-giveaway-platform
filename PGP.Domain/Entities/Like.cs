namespace PGP.Domain.Entities
{
    public class Like
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int PetId { get; set; }
        public virtual Pet Pet { get; set; }
    }
}
