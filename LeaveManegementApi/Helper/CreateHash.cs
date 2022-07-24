using System.Security.Cryptography;

namespace LeaveManegementApi.Helper
{
    public class CreateHash
    {
        public IConfiguration configuration;

        public CreateHash(IConfiguration iconfig)
        {
            configuration = iconfig;
        }

        public static void CreatePassworHash(string password,out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac= new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }


      

        public static bool VerifyPassworHash(string password,  byte[] passwordHash,  byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
