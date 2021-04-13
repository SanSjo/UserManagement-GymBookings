using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement_GymBookings.Models;

namespace UserManagement_GymBookings.Repositories
{
    public interface IGymClassRepository
    {
        Task<IEnumerable<GymClass>> GetAllAsync();
        Task<IEnumerable<GymClass>> GetWithBookingsAsync();
        Task<GymClass> GetAsync(int? id);
        Task<IEnumerable<GymClass>> GetHistoryAsync();
        void Remove(GymClass gymClass);
        void Update(GymClass gymClass);
        void Add(GymClass gymClass);
        bool Any(int id);
        Task<GymClass> FindAsync(int? id);


    }
}