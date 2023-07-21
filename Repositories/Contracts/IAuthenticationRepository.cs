using Entities.Models;

namespace Repositories.Contracts
{
    public interface IAuthenticationRepository
    {
        public bool Register(User user);
        public bool Login(User user);
    }
}
