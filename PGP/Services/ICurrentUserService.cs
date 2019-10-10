namespace PGP.WebUI.Services
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string Role { get; }
    }
}