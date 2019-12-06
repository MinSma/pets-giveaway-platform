using PGP.Domain.Enums;

namespace PGP.Application.Pets
{
    public class PetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string City { get; set; }
        public Gender Gender { get; set; }
        public State State { get; set; }
        public string PhotoCode { get; set; }
    }
}
