using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement_GymBookings.Models
{
    public class ApplicationUserGymClass
    {
        public int GymClassID { get; set; }
        public string ApplicationUserID { get; set; }

        //NavigationProperty
        public GymClass GymClass { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


    }
}
