using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FishbowlSoftware.Planner.Identity.Pages.Account;

public class ProfileModel : PageModel
{
    public IEnumerable<System.Security.Claims.Claim> Claims => User.Claims;
}
