namespace Entities.Exceptions
{
    public class NotUniqueUsernameBadRequestException : BadRequestException
    {
        public NotUniqueUsernameBadRequestException() : base("Kullanıcı adınız eşsiz olmalıdır.")
        {
        }
    }
}
