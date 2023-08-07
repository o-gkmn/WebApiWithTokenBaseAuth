using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObject
{
    public record UserForRegistrationDto
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }

        [EmailAddress(ErrorMessage = "Not a valid email address")]
        public string? Email { get; init; }

        [RegularExpression(@"^(\+\d{1,2}\s?)?[\s.-]?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Not a valid phone number")]
        public string? PhoneNumber { get; init; }
        public ICollection<string>? Roles { get; init; }

        [Required(ErrorMessage = "Username is not empty")]
        public string UserName { get; init; }

        [Required(ErrorMessage = "Password is not empty")]
        public string Password { get; init; }

    }

}
