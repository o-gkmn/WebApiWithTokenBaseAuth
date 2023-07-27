using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;

public class TokenAuthenticationFilter : IAuthorizationFilter
{
    private readonly IAccesTokenManager _accesTokenManager;

    public TokenAuthenticationFilter(IAccesTokenManager accesTokenManager)
    {
        _accesTokenManager = accesTokenManager;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.Filters.Any(item => item is IAllowAnonymousFilter))
        {
            return;
        }

        string token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");

        if (string.IsNullOrEmpty(token))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        try
        {

            if (!_accesTokenManager.ValidateToken(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var principal = _accesTokenManager.GetPrincipal(token);
            context.HttpContext.User = principal;
        }
        catch (SecurityTokenValidationException)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
