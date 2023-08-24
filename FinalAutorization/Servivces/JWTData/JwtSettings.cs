using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FinalAutorization.Servivces.JWTData
{
    public class JwtSettings
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string Secret { get; set; }
        public int TokenExpirationHours { get; set; }

    }
    
}
