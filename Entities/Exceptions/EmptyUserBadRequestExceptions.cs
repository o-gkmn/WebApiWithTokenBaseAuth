namespace Entities.Exceptions
{
    public class EmptyUserBadRequestExceptions : BadRequestException
    {
        public EmptyUserBadRequestExceptions() : base("User mustn't be empty") {}
    }
}
