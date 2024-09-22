using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreSecurity.Pages
{
    [Authorize(Policy = "HRManagerOnly", 
        AuthenticationSchemes = $"{JwtBearerDefaults.AuthenticationScheme},MyCookieAuthentication")]
    public class HRManagerModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
