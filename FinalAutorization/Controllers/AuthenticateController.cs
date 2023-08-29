using FinalAutorization.Models;
using FinalAutorization.Servivces;
using Microsoft.AspNetCore.Mvc;


namespace FinalAutorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IControllerService _controllerService;

        public AuthenticateController(IControllerService controllerService)
        {
            _controllerService = controllerService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var loginUser = await _controllerService.IsLoginSuccess(loginModel);
            if (loginUser.Status)
                return Ok(loginUser);

            return BadRequest(loginUser.Message);
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var value = await _controllerService.IsRegisterSuccess(registerModel);
            if (value.Status)
                return Ok(value);
            return BadRequest(value.Message);
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
