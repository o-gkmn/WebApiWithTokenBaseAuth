using Entities.DataTransferObject;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts
{
    public interface IAuthService
    {
        public Task<IdentityResult> Register(UserForRegistrationDto userForRegistration);
        public Task<bool> Login(UserForLoginDto userForLogin);
        public Task<TokenDto> CreateToken(bool populateExp);
        public Task<TokenDto> RefreshToken(RefreshTokenDto tokenDto);
    }
}
