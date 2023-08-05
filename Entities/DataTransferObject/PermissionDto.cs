namespace Entities.DataTransferObject
{
    public record class PermissionDto
    {
        public string Role { get; init; }
        public string Permission { get; init; }
    }
}
