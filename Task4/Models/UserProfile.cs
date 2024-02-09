using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task4.Models
{
    public class UserProfile : IdentityUser
    {
        public string? Name{ get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime RegistrationTime { get; set; }
        public DateTime LastVisit { get; set; }

        [NotMapped]
        public bool Selected { get; set; } = false;
    }

    public class UserModel
    {
        public bool SelectAll { get; set; }
        public List<UserProfile> Users { get; set; }
    }
}
