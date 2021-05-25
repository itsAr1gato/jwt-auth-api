using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;

namespace Webproj.Services
{
    public interface IPasswordHasher
    {
        string Hash(string password);

        bool Check(string hash, string password);
    }

    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 32;
        private const int KeySize = 64;
        private const int Iterations = 25651224;

        public string Hash(string password)
        {
            if (password.Length >= 6)
            {
                using (var algorithm = new Rfc2898DeriveBytes(password, 
                                                              SaltSize, 
                                                              Iterations, 
                                                              HashAlgorithmName.SHA256))
                {
                    string key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                    string salt = Convert.ToBase64String(algorithm.Salt);

                    return $"{Iterations}.{salt}.{key}";
                }
            }

            throw new FormatException("Password should be at least 6 characters.");
        }

        public bool Check(string hash, string password)
        {
            if (password != null && hash != null)
            {
                string[] parts = hash.Split('.', 3);

                if (parts.Length != 3)
                {
                    throw new FormatException("Unexpected hash format. Should be formatted as `{iterations}.{salt}.{hash}`");
                }

                int iterations = Convert.ToInt32(parts[0]);
                byte[] salt = Convert.FromBase64String(parts[1]);
                byte[] key = Convert.FromBase64String(parts[2]);

                using (var algorithm = new Rfc2898DeriveBytes(password, 
                                                              salt, 
                                                              iterations, 
                                                              HashAlgorithmName.SHA256))
                {
                    byte[] keyToCheck = algorithm.GetBytes(KeySize);

                    bool verified = keyToCheck.SequenceEqual(key);

                    return verified;
                }
            }

            throw new NoNullAllowedException("Password and hash should not be empty.");
        }
    }
}