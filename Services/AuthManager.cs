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
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAccesTokenManager _accesTokenManager;
        private readonly IRefreshTokenManager _refreshTokenManager;

        private User? _user;

        public AuthManager(UserManager<User> manager, IMapper mapper, IAccesTokenManager tokenManager, IRefreshTokenManager refreshTokenManager, IConfiguration configuration, ILoggerService loggerService)
        {
            _userManager = manager;
            _mapper = mapper;
            _accesTokenManager = tokenManager;
            _refreshTokenManager = refreshTokenManager;
            _loggerService = loggerService;
        }

        public async Task<IdentityResult> Register(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);

            if (result.Succeeded)
            {
                _loggerService.LogInformation($"{userForRegistration.UserName} created succesfully");
                var roleResult = await _userManager.AddToRolesAsync(user, userForRegistration.Roles);

                if (roleResult.Succeeded)
                {
                    _loggerService.LogInformation($"{userForRegistration.Roles} succesfully adde to {userForRegistration.UserName}");
                }
                else
                {
                    _loggerService.LogWarning($"{userForRegistration.Roles} failed adde to {userForRegistration.UserName}");

                }
            }

            return result;
        }

        public async Task<bool> Login(UserForLoginDto userForLogin)
        {
            _user = await _userManager.FindByNameAsync(userForLogin.UserName);

            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForLogin.Password));

            if (!result)
            {
                _loggerService.LogWarning($"Wrong email or password for {userForLogin.UserName}");
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
                var user = await _userManager.FindByNameAsync(refreshPrincipal.Identity.Name);
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