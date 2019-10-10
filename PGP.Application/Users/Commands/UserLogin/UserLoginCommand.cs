using MediatR;

namespace PGP.Application.Users.UserLogin
{
    public class UserLoginCommand : IRequest<UserLoginCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
