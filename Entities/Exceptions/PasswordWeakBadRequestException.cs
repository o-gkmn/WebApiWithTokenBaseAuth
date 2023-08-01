namespace Entities.Exceptions
{
    public class PasswordWeakBadRequestException : BadRequestException
    {
        public PasswordWeakBadRequestException() : base("Password must be higher then 6 characters") { }
    }
}
