using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthHandler
{
    public class JwtHandler
    {
        public const string SECRET = "ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM";
        //public const string ISSUER = "finalautorization";
        //public const string AUDIENCE = "epa-webapi";

        public JwtHandler()
        {
            
        }

        public string GenerateToken(string userName, IEnumerable<string> roles)
        {
            var authClaims = new List<Claim>
                    {
                        new Claim (ClaimTypes.Name, userName),
                    };

            foreach (var userRole in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = new JwtSecurityToken(
                       //issuer: ISSUER,
                       //audience: AUDIENCE,
                        expires: DateTime.Now.AddHours(3),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(
                                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET)),
                                               SecurityAlgorithms.HmacSha256)
                        );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}