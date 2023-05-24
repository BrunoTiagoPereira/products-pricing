using System.Security.Cryptography;
using System.Text;

namespace ProductsPricing.Core.ValueObjects
{
    public class Password : ValueObject
    {
        public string Hash { get; private set; }

        protected Password() { }
        public Password(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            Hash = GetMD5HashFromPassword(password);
        }

        private static string GetMD5HashFromPassword(string password)
        {
            using MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hash;
        }
    }
}