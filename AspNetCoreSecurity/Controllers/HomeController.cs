using AspNetCoreSecurity.Pages.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AspNetCoreSecurity.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult GetJWTBearerToken([FromBody] Credential credential)
        {
            // Step 1: validate the username/password
            // By pass this step

            // Step 2: create a token
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Email, "admin@email.com"),
                new Claim("Department", "HR"),
                new Claim("Admin", "true"),
                new Claim("HRManager", "true"),
                new Claim("EmploymentDate", "2024-01-25")
            };

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
               .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        [HttpPost]
        public async Task GetBearerToken([FromBody] Credential credential)
        {
            var claimsForToken = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Email, "admin@email.com"),
                new Claim("Department", "HR"),
                new Claim("Admin", "true"),
                new Claim("HRManager", "true"),
                new Claim("EmploymentDate", "2024-01-25")
            };

            var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(
                claimsForToken,
                BearerTokenDefaults.AuthenticationScheme));

            await HttpContext.SignInAsync(BearerTokenDefaults.AuthenticationScheme, claimPrincipal);
        }
    }
}
