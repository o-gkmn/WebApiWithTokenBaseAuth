using AutoMapper;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;

namespace Services
{
    public class AuthManager : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _manager;

        private User? _user;
        
        public AuthManager(UserManager<User> manager, IMapper mapper = null)
        {
            _manager = manager;
            _mapper = mapper;
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
    } 
}
