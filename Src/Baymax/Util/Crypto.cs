using System;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Baymax.Util
{
    public static class Hash
    {
        public static string Create(string value, string salt)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(value);
            }
            
            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new ArgumentNullException(salt);
            }

            byte[] hashBytes = KeyDerivation.Pbkdf2(password: value,
                                                    salt: Encoding.UTF8.GetBytes(salt),
                                                    prf: KeyDerivationPrf.HMACSHA512,
                                                    iterationCount: 100000,
                                                    numBytesRequested: 256 / 8);

            return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
        }

        public static bool Validate(string value, string salt, string hash)
        {
            return Create(value, salt) == hash;
        }
    }
}