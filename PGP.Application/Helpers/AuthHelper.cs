using System;
using System.Security.Cryptography;
using System.Text;

namespace PGP.Application.Helpers
{
    public class AuthHelper
    {
        internal static string GetPasswordHash(string password)
        {
            var hash = SHA256.Create();
            var passwordInBytes = Encoding.UTF8.GetBytes(password);
            var hashedPasswordInBytes = hash.ComputeHash(passwordInBytes);
            return BitConverter.ToString(hashedPasswordInBytes).Replace("-", string.Empty);
        }
    }
}
