﻿namespace PGP.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
