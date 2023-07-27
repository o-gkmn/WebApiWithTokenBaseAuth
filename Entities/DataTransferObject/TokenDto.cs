namespace Entities.DataTransferObject
{
    public record TokenDto
    {
        public string AccesToken { get; init; }
        public string RefreshToken { get; init; }
    }
}
