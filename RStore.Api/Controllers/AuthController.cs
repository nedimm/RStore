using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RStore.Api.Data;
using RStore.Api.Dto.User;
using RStore.Api.Static;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<ApiUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
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
    public async Task<ActionResult<AuthResponse>> Login(LoginUserDto userDto)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);
            var passwordIsValid = await _userManager.CheckPasswordAsync(user, userDto.Password);
            if (user == null || passwordIsValid == false)
            {
                return Unauthorized(userDto);
            }

            var tokenString = await GenerateTokenAsync(user);
            var response = new AuthResponse
            {
                Email = userDto.Email,
                Token = tokenString,
                UserId = user.Id
            };

            return Accepted(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Could not login {userDto.Email}", ex.Message);
            return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
        }
    }

    private async Task<string> GenerateTokenAsync(ApiUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

        var userClaims = await _userManager.GetClaimsAsync(user);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(CustomClaimTypes.Uid, user.Id),
        }
        .Union(userClaims)
        .Union(roleClaims);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(Convert.ToInt32(_configuration["JwtSettings:Duration"])),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
