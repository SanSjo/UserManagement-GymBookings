using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement_GymBookings.Models.ViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<GymClassViewModel> GymClasses { get; set; }
        public bool ShowHistory { get; set; }
    }
}
