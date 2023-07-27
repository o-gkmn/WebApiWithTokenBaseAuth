using Entities.Models;
using System.Security.Claims;

namespace Services.Contracts
{
    public interface IRefreshTokenManager
    {
        public Task<string> GenerateToken(User user);
        public ClaimsPrincipal? GetPrincipal(string token);
        public bool ValidateToken(string token);
    }
}
