using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class User
    {
        public int id;

        [Required(ErrorMessage = "Username is required")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public String Password { get; set; }

        public String? Email { get; set; }
        public String? PhoneNumber { get; set; }
        public String? FirstName { get; set; }
        public String? LastName { get; set;}
    }
}
