namespace Entities.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string userName) : base($"User with usernamme : {userName} could not found")
        {
        }
    }
}
