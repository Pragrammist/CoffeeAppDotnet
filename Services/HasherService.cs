using Services.Contracts;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class HasherService : IHasherService
    {
        public string GetHashForToken<TValue>(TValue value)
        {
            var hashCodeOfValue = value?.GetHashCode() ?? throw new NullReferenceException("input value is null");

            var bytes = BitConverter.GetBytes(hashCodeOfValue);
            var hashedBytes = MD5.HashData(bytes);
            var base64 = Convert.ToBase64String(hashedBytes);
            return base64;
        }

        public string HashPassword(string password) =>
            Encoding.UTF8.GetString(
                MD5.HashData(
                    Encoding.UTF8.GetBytes(password)
                )
            );
    }

}