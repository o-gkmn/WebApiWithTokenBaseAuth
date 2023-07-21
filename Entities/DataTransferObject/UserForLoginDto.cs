using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObject
{
    public record UserForLoginDto
    {
        [Required(ErrorMessage = "Username is not empty")]
        public String? UserName { get; init; }
        [Required(ErrorMessage = "Username is not empty")]
        public String? Password { get; init; }
    }
}
