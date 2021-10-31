using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BicycleShop.Config
{
    public class AuthOptions
    {
        public const string ISSUER = "BicycleServer";
        public const string AUDIENCE = "BicycleClient";
        const string KEY = "SecretKeyWithResult";
        public const int LIFETIME = 5;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
