using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IAuthService _authService;

        public ServiceManager(IAuthService authService)
        {
            _authService = authService;
        }

        public IAuthService AuthenticationService => _authService;
    }
}
