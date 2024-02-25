using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FishbowlSoftware.Planner.Identity.Pages.Account;

public class SignInModel : PageModel
{
    public Task OnGetAsync(string returnUrl = "/")
    {
        return LoginAsync(returnUrl);
    }
    
    private async Task LoginAsync(string returnUrl = "/")
    {
        var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri(returnUrl)
            .Build();

        await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    }
}
