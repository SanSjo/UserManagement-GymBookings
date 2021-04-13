using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement_GymBookings.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; }
        public DateTime TimeOfRegistration { get; set; }
        public virtual ICollection<ApplicationUserGymClass> AttendedClasses { get; set; }
    }
}
