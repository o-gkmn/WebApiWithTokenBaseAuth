namespace Entities.Exceptions
{
    public class RoleNotFoundForUser : NotFoundException
    {
        public RoleNotFoundForUser(string roleName, string userName) : base($"{roleName} could not found for {userName}")
        {
        }
    }
}
