using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RStore.Api.Data;
using RStore.Api.Dto.User;

namespace RStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<ApiUser> _userManager;

    public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<ApiUser> userManager)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterUserDto userDto)
    {
        try
        {
            var user = _mapper.Map<ApiUser>(userDto);
            user.UserName = userDto.Email;
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded == false)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                _logger.LogError($"Could not create user {userDto.Email}");
                return BadRequest(ModelState);
            }

            result = await _userManager.AddToRoleAsync(user, userDto.Role);
            if (result.Succeeded == false)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                _logger.LogError($"Could not add user {userDto.Email} to role {userDto.Role}");
                return BadRequest(ModelState);
            }

            return Accepted();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Could not register {userDto.Email}", ex.Message);
            return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
        }
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginUserDto userDto)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);
            var passwordIsValid = await _userManager.CheckPasswordAsync(user, userDto.Password);
            if (user == null || passwordIsValid == false)
            {
                return NotFound();
            }
            return Accepted();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Could not login {userDto.Email}", ex.Message);
            return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
        }
        return Ok();
    }
}
