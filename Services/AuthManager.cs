using AutoMapper;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Contracts;

namespace Services
{
    public class AuthManager : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _manager;
        private readonly IAccesTokenManager _accesTokenManager;
        private readonly IRefreshTokenManager _refreshTokenManager;

        private User? _user;

        public AuthManager(UserManager<User> manager, IMapper mapper, IAccesTokenManager tokenManager, IRefreshTokenManager refreshTokenManager, IConfiguration configuration = null)
        {
            _manager = manager;
            _mapper = mapper;
            _accesTokenManager = tokenManager;
            _refreshTokenManager = refreshTokenManager;
        }

        public async Task<IdentityResult> Register(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);

            var result = await _manager.CreateAsync(user, userForRegistration.Password);

            if (result.Succeeded)
                await _manager.AddToRolesAsync(user, userForRegistration.Roles);

            return result;
        }

        public async Task<bool> Login(UserForLoginDto userForLogin)
        {
            _user = await _manager.FindByNameAsync(userForLogin.UserName);

            var result = (_user != null && await _manager.CheckPasswordAsync(_user, userForLogin.Password));

            if (!result)
            {
                Console.WriteLine("Authentication Failed. Wrong user name or password");
            }

            return result;
        }

        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            var accesToken = await _accesTokenManager.GenerateToken(_user);
            var refreshToken = await _refreshTokenManager.GenerateToken(_user);

            return new TokenDto
            {
                AccesToken = accesToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<TokenDto?> RefreshToken(RefreshTokenDto tokenDto)
        {
            if (_refreshTokenManager.ValidateToken(tokenDto.RefreshToken))
            {
                var refreshPrincipal = _refreshTokenManager.GetPrincipal(tokenDto.RefreshToken);
                var user = await _manager.FindByNameAsync(refreshPrincipal.Identity.Name);
                var accesToken = await _accesTokenManager.GenerateToken(user);
                var refreshToken = await _refreshTokenManager.GenerateToken(user);
                return new TokenDto
                {
                    AccesToken = accesToken,
                    RefreshToken = refreshToken
                };
            }
            return null;
        }
    }
}