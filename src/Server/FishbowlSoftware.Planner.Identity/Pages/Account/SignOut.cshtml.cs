using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Okta.AspNetCore;

namespace FishbowlSoftware.Planner.Identity.Pages.Account;

public class SignOutModel : PageModel
{
    public IActionResult OnPost()
    {
        return new SignOutResult(
            new[]
            {
                OktaDefaults.MvcAuthenticationScheme,
                CookieAuthenticationDefaults.AuthenticationScheme,
            },
            new AuthenticationProperties { RedirectUri = "/Index" });
    }
}
