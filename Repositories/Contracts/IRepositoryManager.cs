namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        public IAuthenticationRepository Authentication { get; }
    }
}
