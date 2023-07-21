using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObject
{
    public record UserForRegistrationDto
    {
        public String? FirstName { get; init; }
        public String? LastName { get; init; }
        public String? Email { get; init; }
        public String? PhoneNumber { get; init; }

        [Required(ErrorMessage = "Username is not empty")]
        public String UserName { get; init; }

        [Required(ErrorMessage = "Password is not empty")]
        public String Password { get; init; }

    }

}
