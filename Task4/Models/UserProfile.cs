using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task4.Models
{
    public class UserProfile : IdentityUser
    {
        public bool IsActive { get; set; }
        public DateTime RegistrationTime { get; set; }
        public DateTime LastVisit { get; set; }

        [NotMapped]
        public bool Selected { get; set; }
    }

    public class UserModel
    {
        public bool SelectAll { get; set; }
        public List<UserProfile> Users { get; set; }
    }
}
