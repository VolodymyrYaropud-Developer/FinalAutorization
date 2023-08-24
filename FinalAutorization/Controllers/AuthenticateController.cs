using FinalAutorization.Context;
using FinalAutorization.Models;
using FinalAutorization.Servivces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace FinalAutorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        //private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public AuthenticateController(UserManager<User> _userManager,
            RoleManager<IdentityRole> _roleManager, IConfiguration _configuration)
        {
            userManager = _userManager;
            //roleManager = _roleManager;
            configuration = _configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            loginModel.Email = "user@example.com";
            loginModel.Password = "Hello123321Hello123321";
            var loginUser = ControllerServise.IsLoginSuccess(loginModel, userManager, configuration);
            //user@example.com
            //Hello123321Hello123321
            
            if (loginUser.Result.Status == true)
                return Ok(loginUser.Result);

            return BadRequest();
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var registerUser = ControllerServise.IsRegisterSuccess(registerModel, userManager);

            if (registerUser.Result.Status == true)
                return Ok(registerUser.Result);
            return BadRequest();
        }


        //[HttpPost]
        //[Route("register-admin")]
        //public async Task<IActionResult> RegisterAdmin(RegisterModel registerModel)
        //{
        //    var userExists = await userManager.FindByNameAsync(registerModel.userName);
        //    if (userExists != null)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response() { Status = false, Message = "user already exists" });
        //    User user = new User()
        //    {
        //        Email = registerModel.Email,
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //        UserName = registerModel.userName
        //    };
        //    var result = await userManager.CreateAsync(user, registerModel.Password);

        //    if (!result.Succeeded)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = false, Message = "User creation failed! Please check user details and try again" });

        //    if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
        //        await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        //    if (!await roleManager.RoleExistsAsync(UserRoles.User))
        //        await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        //    if (await roleManager.RoleExistsAsync(UserRoles.Admin))
        //    {
        //        await userManager.AddToRoleAsync(user, UserRoles.Admin);
        //    }

        //    return Ok(new Response { Status = true, Message = "User created successfully!" });
        //}
    }
}
