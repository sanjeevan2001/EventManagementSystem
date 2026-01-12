using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EventManagement.Infrastrure.Helpers
{
    public static class PasswordHelper
    {// Create Password Hash + Salt
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required.", nameof(password));

            using (var hmac = new HMACSHA512()) // SHA512 is secure
            {
                passwordSalt = hmac.Key; // Random key
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        // Verify Password
        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (string.IsNullOrEmpty(password)) return false;
            if (storedHash == null || storedSalt == null) return false;
            if (storedHash.Length != 64) return false; // HMACSHA512 output size
            if (storedSalt.Length != 128) return false; // HMACSHA512 key size

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
            }
        }
    }
}
