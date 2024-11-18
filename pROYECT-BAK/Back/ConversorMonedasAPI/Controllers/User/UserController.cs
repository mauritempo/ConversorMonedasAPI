using Data.entidades;
using DTO.SUBS;
using DTO.USER;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConversorMonedasAPI.Controllers.User
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly IUserServices _userService;

        public UserController(IConfiguration config, IUserServices userService)
        {
            _config = config;
            _userService = userService;

        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] UserCredentials User)
        {
            var user = _userService.ValidateUser(User.Username, User.Password);

            if (user is null)
                return Unauthorized();

            //Paso 2: Crear el token
            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));

            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.Id.ToString()));

            claimsForToken.Add(new Claim("Name", user.Username.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
              _config["Authentication:Issuer"],
              _config["Authentication:Audience"],
              claimsForToken,
              DateTime.UtcNow,
              DateTime.UtcNow.AddHours(1),
              credentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            // Devolver el token en formato JSON
            return Ok(new { token = tokenToReturn });

        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegistrationDTO registrationDto)
        {

            var user = _userService.CreateUser(registrationDto);

            if (user == null)
            {
                return BadRequest("User registration failed.");
            }

            return Ok(new { Message = "User registered successfully", UserId = user.Id });
        }
        [Authorize]
        [HttpGet("usuarios/{username}")]
        public IActionResult GetUserDetails(string username)
        {
            var user = _userService.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }

            return Ok(new { username = user.Username, Sub = user.SubscriptionId });
        }

    }
    
}
