using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PackyAPI.Data;
using PackyAPI.Models;

namespace PackyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        //private readonly SignInManager<ApiUser> _signinManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ApiUser> userManager, 
                                // SignInManager<ApiUser> signinManager, 
                                 ILogger<AccountController> logger, 
                                 IMapper mapper)
        {
            _userManager = userManager;
            //_signinManager = signinManager; 
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("regiser")]
        public async Task<IActionResult> Register([FromBody] UserDTO userdto)
        {
            _logger.LogInformation($"Registration attempt recieved for {userdto.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    var user = _mapper.Map<ApiUser>(userdto);
                    user.UserName = userdto.Email;
                    var result = await _userManager.CreateAsync(user, userdto.Password);
                    if (!result.Succeeded)
                    {
                        foreach(var error in result.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }
                        //throw new Exception("Registration Attempt failed");
                        return BadRequest(ModelState);
                    }
                    await _userManager.AddToRolesAsync(user,userdto.Roles);
                    return Accepted();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,$"Something went wrong while registering{nameof(Register)}");
                    return StatusCode(500, $"Something went wrong while registering{nameof(Register)}");
                }
            }
        }
        //[HttpPost]
        //[Route("login")]
        //public async Task<IActionResult> Login([FromBody] LoginDTO logindto)
        //{
        //    _logger.LogInformation($"Login attempt recieved for {logindto.Email}");
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    else
        //    {
        //        try
        //        {
        //            var result = await _signinManager.PasswordSignInAsync(logindto.Email, logindto.Password, false, false);
        //            if (!result.Succeeded)
        //            {
        //                return Unauthorized(logindto);
        //            }
        //            return Accepted();
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, $"Something went wrong while logging in{nameof(Register)}");
        //            return StatusCode(500, $"Something went wrong while logging in{nameof(Register)}");
        //        }
        //    }
        //}
    }
}
