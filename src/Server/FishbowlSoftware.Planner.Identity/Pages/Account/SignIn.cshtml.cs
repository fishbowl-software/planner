using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Okta.AspNetCore;

namespace FishbowlSoftware.Planner.Identity.Pages.Account;

public class SignInModel : PageModel
{
    public IActionResult OnGet()
    {
        if (HttpContext.User.Identity is { IsAuthenticated: false })
        {
            return Challenge(OktaDefaults.MvcAuthenticationScheme);
        }

        return Redirect("/Index");
    }
}
