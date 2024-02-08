using Microsoft.AspNetCore.Identity;

namespace Task4.Models
{
    public class UserProfile : IdentityUser
    {
        public bool IsActive { get; set; }
        public DateTime RegistrationTime { get; set; }
        public DateTime LastVisit { get; set; }
    }
}
