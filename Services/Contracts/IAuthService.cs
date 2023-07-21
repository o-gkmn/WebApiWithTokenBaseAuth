using Entities.DataTransferObject;

namespace Services.Contracts
{
    public interface IAuthService
    {
        public bool Register(UserForRegistrationDto userForRegistration);
        public bool Login(UserForLoginDto userForLogin);
    }
}
