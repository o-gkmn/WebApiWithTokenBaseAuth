using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObject
{
    public record UserForRegistrationDto
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public ICollection<string>? Roles { get; init; }

        [Required(ErrorMessage = "Username is not empty")]
        public string UserName { get; init; }

        [Required(ErrorMessage = "Password is not empty")]
        public string Password { get; init; }

    }

}
