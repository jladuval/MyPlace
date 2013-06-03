namespace Security.Services
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    using Base.DDD.Domain.Annotations;

    [DomainService]
    public class CryptoService : ICryptoService
    {
        private const int SaltSize = 128 / 8;

        public string GenerateSalt()
        {
            return Convert.ToBase64String(GenerateSaltInternal());
        }

        public string GenerateRandomHash()
        {
            return Convert.ToBase64String(GenerateSaltInternal());
        }

        public string Hash(string plaintext, string salt)
        {
            if (plaintext == null)
            {
                throw new ArgumentNullException("plaintext");
            }

            if (salt == null)
            {
                throw new ArgumentNullException("salt");
            }
            var hashedSaltedPlaintext = GenerateSaltedHash(Encoding.UTF8.GetBytes(plaintext),
                                                           Encoding.UTF8.GetBytes(salt));
            return Convert.ToBase64String(hashedSaltedPlaintext);
        }

        public bool CheckPassword(string hashedpassword, string plaintext, string salt)
        {
            if (hashedpassword == null)
            {
                throw new ArgumentNullException("hashedpassword");
            }

            if (plaintext == null)
            {
                throw new ArgumentNullException("plaintext");
            }

            if (salt == null)
            {
                throw new ArgumentNullException("salt");
            }

            var hashedSaltedPlaintext = GenerateSaltedHash(Encoding.UTF8.GetBytes(plaintext),
                                                           Encoding.UTF8.GetBytes(salt));
            return CompareByteArrays(Convert.FromBase64String(hashedpassword), hashedSaltedPlaintext);
        }

        private static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            var plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (var i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (var i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        private static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static byte[] GenerateSaltInternal()
        {
            var buf = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(buf);
            }

            return buf;
        }
    }
}
