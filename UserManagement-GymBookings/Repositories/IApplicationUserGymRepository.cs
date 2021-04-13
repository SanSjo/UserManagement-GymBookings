using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement_GymBookings.Models;

namespace UserManagement_GymBookings.Repositories
{
    public interface IApplicationUserGymRepository
    {
        void Add(ApplicationUserGymClass attending);
        Task<ApplicationUserGymClass> GetAttending(int? id, string userId);
        Task<IEnumerable<GymClass>> GetBookingsAsync(string userId);
        void Remove(ApplicationUserGymClass attending);
    }
}