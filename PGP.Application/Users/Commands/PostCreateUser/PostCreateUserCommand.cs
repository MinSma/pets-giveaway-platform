using MediatR;

namespace PGP.Application.Users.Commands.PostCreateUser
{
    public class PostCreateUserCommand : IRequest<Unit>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
