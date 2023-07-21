using Repositories.Contracts;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public RepositoryManager(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        public IAuthenticationRepository Authentication => _authenticationRepository;
    }
}
