using System.Security.Cryptography;

namespace HashWithSalt
{
    class HashWorker
    {
        public HashWorker()
        {
        }

        public byte[] GetSalt(int length)
        {
            byte[] salt = new byte[length];

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(salt);

            return salt;
        }

        public byte[] ComputeIteratedHash(string password, byte[] salt, int iterations = 50, int hashByteSize = 16)
        {
            using (Rfc2898DeriveBytes hashGen = new Rfc2898DeriveBytes(password, salt))
            {
                hashGen.IterationCount = iterations;
                return hashGen.GetBytes(hashByteSize);
            }
        }

    }
}
