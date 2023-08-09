using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;

namespace Presentation.ActionFilters
{
    public class AutherizationFilter : Attribute, IAsyncAuthorizationFilter
    {
        private readonly ILoggerService _logger;
        private readonly IAccesTokenManager _accessTokenManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly string? _policy;

        public AutherizationFilter(IAccesTokenManager accessTokenManager, UserManager<User> userManager, RoleManager<Role> roleManager, string? Policy = null, ILoggerService logger = null)
        {
            _accessTokenManager = accessTokenManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _policy = Policy;
            _logger = logger;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                return;
            }

            string token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                _logger.LogError("Token is empty");
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                if (!_accessTokenManager.ValidateToken(token))
                {
                    context.Result = new UnauthorizedResult();
                    _logger.LogError($"Failed to validate token: {token}");
                    return;
                }

                var principal = _accessTokenManager.GetPrincipal(token);

                if (principal == null)
                {
                    _logger.LogError("Principal is null");
                    context.Result = new NotFoundResult();
                    return;
                }

                if(_policy is null)
                {
                    context.HttpContext.User = principal;
                    return;
                }

                var user = await _userManager.FindByNameAsync(principal.Identity.Name);

                if (user == null)
                {
                    _logger.LogError($"{principal.Identity.Name} could not found");
                    context.Result = new NotFoundResult();
                    return;
                }

                var roles = await _userManager.GetRolesAsync(user);

                if (roles is null)
                {
                    context.Result = new ForbidResult();
                    return;
                }

                foreach (var r in roles)
                {
                    var role = await _roleManager.FindByNameAsync(r);
                    var claims = await _roleManager.GetClaimsAsync(role);

                    foreach (var c in claims)
                    {
                        if (c.Type == _policy)
                        {
                            context.HttpContext.User = principal;
                            return;
                        }
                    }
                }

                context.Result = new ForbidResult();
                return;
            }
            catch (SecurityTokenValidationException)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
