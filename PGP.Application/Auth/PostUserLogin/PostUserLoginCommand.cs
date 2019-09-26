using MediatR;

namespace PGP.Application.Auth.PostUserLogin
{
    public class PostUserLoginCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
