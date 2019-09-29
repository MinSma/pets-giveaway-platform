using MediatR;

namespace PGP.Application.Users.Commands.PutUpdateUser
{
    public class PutUpdateUserCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
