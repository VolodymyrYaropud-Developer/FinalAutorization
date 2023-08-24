using FinalAutorization.Context;
using FinalAutorization.Models;
using FinalAutorization.Servivces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace FinalAutorization.Controllers
{

    public class TestService
    {

    }
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        //private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private TestService testService;
        private IControllerService _controllerService;

        public AuthenticateController(UsersDBContext usersContext,UserManager<User> _userManager,
            RoleManager<IdentityRole> _roleManager, IConfiguration _configuration, IControllerService controllerService)
        {
            userManager = _userManager;
            //roleManager = _roleManager;
            configuration = _configuration;
            _controllerService = controllerService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var loginUser = await _controllerService.IsLoginSuccess(loginModel);
            if (loginUser.Status == true)
                return Ok(loginUser);

            return BadRequest();
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var value = await _controllerService.IsRegisterSuccess(registerModel);
                if (value.Status == true)
                return Ok(value);
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
