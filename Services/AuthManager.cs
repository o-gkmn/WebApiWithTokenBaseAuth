using AutoMapper;
using Entities.DataTransferObject;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class AuthManager : IAuthService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public AuthManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public bool Register(UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration is null)
                throw new EmptyUserBadRequestExceptions();

            if (userForRegistration.Password.Length < 6)
                throw new PasswordWeakBadRequestException();

            var user = _mapper.Map<User>(userForRegistration);
            var result = _manager.Authentication.Register(user);
            return result;
        }

        public bool Login(UserForLoginDto userForLogin)
        {
            if(userForLogin is null)
                throw new EmptyUserBadRequestExceptions();

            var user = _mapper.Map<User>(userForLogin);
            var result = _manager.Authentication.Login(user);
            return result;
        }

    }
}
