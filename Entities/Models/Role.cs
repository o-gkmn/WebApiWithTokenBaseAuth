using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class Role : IdentityRole
    {
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
    }
}
