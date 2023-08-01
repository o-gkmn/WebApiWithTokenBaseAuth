using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IAuthService _authService;
        private readonly IRoleService _roleService;

        public ServiceManager(IAuthService authService, IRoleService roleService)
        {
            _authService = authService;
            _roleService = roleService;
        }

        public IAuthService AuthenticationService => _authService;

        public IRoleService RoleService => _roleService;
    }
}
