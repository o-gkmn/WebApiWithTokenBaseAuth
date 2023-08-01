using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObject
{
    public record RoleDtoForInsertion
    {
        [Required(ErrorMessage = "Role name is required it must not be empty")]
        public string Name { get; init; }
    }
}
