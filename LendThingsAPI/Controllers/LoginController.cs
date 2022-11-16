using LendThingsAPI.Configuration;
using LendThingsAPI.DTO;
using LendThingsAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LendThingsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        public UserManager<User> UserManager { get; }
        public IOptions<JwtOptions> Config { get; }

        public LoginController(UserManager<User> userManager, IOptions<JwtOptions> config)
        {
            UserManager = userManager;
            Config = config;
        }


        [HttpPost()]
        [Route("Login")]
        async public Task<IActionResult> Login([FromBody]UserForLoginDTO userToLogIn)
        {
            var user = await UserManager.FindByNameAsync(userToLogIn.UserName);

            if (!await VerifyIdentity(user, userToLogIn))
            {
                return Forbid();
            };

            var claims = await GenerateJWTClaims(user);

            var jwt = GenerateJWT(claims);

            return Ok(new
            {
                AccessToken = jwt
            });
        }

        async private Task<bool> VerifyIdentity(User user, UserForLoginDTO userToLogIn)
        {
            if (user is null || !await UserManager.CheckPasswordAsync(user, userToLogIn.Password))
            {
                return false;
            }
            return true;
        }

        async private Task<List<Claim>> GenerateJWTClaims(User user)
        {
            var roles = await UserManager.GetRolesAsync(user);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}")
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private string GenerateJWT(List<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.Value.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: Config.Value.Issuer,
                audience: Config.Value.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(720),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
