namespace Entities.Exceptions
{
    public class NotUniqueUsernameBadRequestException : BadRequestException
    {
        public NotUniqueUsernameBadRequestException() : base("Username must be unique")
        {
        }
    }
}
