using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace WebApiLogin.Models
{
    public class HashHelper
    {
        public static string GerarHash(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(senha);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
