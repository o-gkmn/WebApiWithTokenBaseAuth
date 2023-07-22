using Entities.DataTransferObject;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts
{
    public interface IAuthService
    {
        public Task<IdentityResult> Register(UserForRegistrationDto userForRegistration);
        public Task<bool> Login(UserForLoginDto userForLogin);
    }
}
