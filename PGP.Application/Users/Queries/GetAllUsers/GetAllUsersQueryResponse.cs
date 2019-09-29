namespace PGP.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MainPhotoUrl { get; set; }
    }
}
