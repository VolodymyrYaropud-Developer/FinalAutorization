using FinalAutorization.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinalAutorization.Servivces.JWTData;

namespace FinalAutorization.Servivces
{
    public  class ControllerServise : IControllerService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly JwtSettings _jwtSettings;

        public ControllerServise(UserManager<User> userManager, IConfiguration config)
        {
            _config = config;
            _userManager = userManager;
        }

        public async Task<Response> IsLoginSuccess(LoginModel loginModel)
        {
            try
            {                                                                   //user@example.com        
                var user = await _userManager.FindByEmailAsync(loginModel.Email);//Hello123321Hello123321
                if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var authClaims = new List<Claim>
                    {
                    new Claim (ClaimTypes.Name, user.UserName),
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                    //var _jwtSettings = new AuthenticateController();
                    var token = new JwtSecurityToken(
                        issuer: _config["JWT:ValidIssuer"],
                        audience: _config["JWT:ValidAudience"],
                        expires:DateTime.Now.AddHours(3),
                        claims: authClaims,
                        signingCredentials: new  SigningCredentials(
                                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"])),
                                               SecurityAlgorithms.HmacSha256)
                        );

                    return new Response
                    {
                        Status = true,
                        Message = new JwtSecurityTokenHandler().WriteToken(token),
                        
                    };
                }
                return new Response
                {
                    Status = false
                };
            }
            catch (Exception ex)
            {

                return new Response
                {
                    Status = false,
                    Message = ex.Message
                };
            }
        }
        //public static JwtSecurityToken ExampleAction(List<Claim> authClaims)
        //{
        //    var validIssuer = _jwtSettings.ValidIssuer;
        //    var validAudience = _jwtSettings.ValidAudience;
        //    var tokenExpirationHours = DateTime.Now.AddHours(Convert.ToInt16(_jwtSettings.TokenExpirationHours));
        //    var secret = _jwtSettings.Secret;
        //    var claim = authClaims;


        //    return new JwtSecurityToken(
        //        issuer: validIssuer,
        //        audience: validAudience,
        //        expires: tokenExpirationHours,
        //        claims: authClaims,
        //        signingCredentials: new SigningCredentials(
        //                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"])),
        //                        SecurityAlgorithms.HmacSha256));

        //}


        public async Task<Response> IsRegisterSuccess(RegisterModel registerModel)
        {
            try
            {
                var userExist = await _userManager.FindByEmailAsync(registerModel.Email);
                if (userExist != null)
                    return new Response
                    {
                        Status = false,
                        Message = "User alredy exists"
                    };

                User user = new User()
                {
                    Email = registerModel.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = registerModel.userName
                };

                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (!result.Succeeded)
                    return new Response
                    {
                        Status = false,
                        Message = "User creation failed! Please check user details and try again."
                    };

                return new Response
                {
                    Status = true,
                    Message = "User created successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Status = false,
                    Message = ex.Message
                };
            }
        }
    }
}
