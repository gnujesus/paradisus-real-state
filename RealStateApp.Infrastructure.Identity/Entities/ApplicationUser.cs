using Microsoft.AspNetCore.Identity;

namespace RealStateApp.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[]? Image {  get; set; }
        public bool IsActive { get; set; }
    }
}
