using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PGP.Application.Exceptions;
using PGP.Application.Helpers;
using PGP.Persistence;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Users.PostUserLogin
{
    public class PostUserLoginCommandHandler : IRequestHandler<PostUserLoginCommand, PostUserLoginCommandResponse>
    {
        private readonly IPGPDbContext _context;
        private readonly AppSettings _appSettings;

        public PostUserLoginCommandHandler(IPGPDbContext context, IOptions<AppSettings> appsettings)
        {
            _context = context;
            _appSettings = appsettings.Value;
        }

        public async Task<PostUserLoginCommandResponse> Handle(PostUserLoginCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = AuthHelper.GetPasswordHash(request.Password);

            var authorize = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email.Equals(request.Email) && x.Password.Equals(hashedPassword));

            if (authorize == null)
            {
                throw new UnauthorizedException("Login failed.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, authorize.Id.ToString()),
                    new Claim(ClaimTypes.Role, authorize.Role.Title)
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new PostUserLoginCommandResponse
            {
                JwtToken = tokenHandler.WriteToken(token)
            };
        }
    }
}
