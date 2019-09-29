using MediatR;

namespace PGP.Application.Users.PostUserLogin
{
    public class PostUserLoginCommand : IRequest<PostUserLoginCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
