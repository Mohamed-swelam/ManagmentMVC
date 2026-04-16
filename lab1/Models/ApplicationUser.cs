using Microsoft.AspNetCore.Identity;

namespace lab1.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Address { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int Age { get; set; }
    }
}
