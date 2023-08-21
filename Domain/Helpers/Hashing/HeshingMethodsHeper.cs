using System.Security.Cryptography;
using System.Text;


namespace Domain.Helpers.Hashing
{
    public static class HeshingMethodsHeper
    {
        public static string GetRandomHash<TValue>(this TValue value)
        {
            var hashCodeOfValue = value?.GetHashCode() ?? throw new NullReferenceException("input value is null");

            var bytes = BitConverter.GetBytes(hashCodeOfValue);
            var hashedBytes = MD5.HashData(bytes);
            var base64 = Convert.ToBase64String(hashedBytes);
            return base64;
        }

        

        public static string Hash(this string password) =>
            Encoding.UTF8.GetString(
                MD5.HashData(
                    Encoding.UTF8.GetBytes(password)
                )
            );
    }
}
