using MediatR;

namespace PGP.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoCode { get; set; }
    }
}
