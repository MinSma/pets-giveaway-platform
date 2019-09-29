namespace PGP.Domain.Entities
{
    public class Like
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int PetId { get; set; }
        public Pet Pet { get; set; }
    }
}
