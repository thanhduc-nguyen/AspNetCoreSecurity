using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreSecurity.Pages
{
    [Authorize(Policy = "MustBelongToHRDepartment", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HumanResourcesModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
