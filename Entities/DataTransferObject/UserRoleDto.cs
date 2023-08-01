using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObject
{
    public record UserRoleDto
    {
        [Required(ErrorMessage = "Username cannot be empty")]
        public string userName { get; init; }
        [Required(ErrorMessage = "Role name cannot be empty")]
        public string roleName { get; init; }
    }
}
