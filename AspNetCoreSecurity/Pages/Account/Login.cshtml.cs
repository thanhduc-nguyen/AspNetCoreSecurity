using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace AspNetCoreSecurity.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Verify the credential
            if (Credential.Username == "admin" && Credential.Password == "password")
            {
                // Creating the security context
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@email.com"),
                    new Claim("Department", "HR"),
                    new Claim("Admin", "true"),
                    new Claim("HRManager", "true")
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuthentication");
                var claimsPrincipal = new ClaimsPrincipal(identity);

                // Serialize the claims plincipal into a string and encrypt that string, save that as a cookie in the HttpContext
                await HttpContext.SignInAsync("MyCookieAuthentication", claimsPrincipal);

                return RedirectToPage("/Index");
            }

            return Page();
        }
    }

    public class Credential
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
