using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement_GymBookings.Data;
using UserManagement_GymBookings.Models;

namespace UserManagement_GymBookings.Repositories
{
    public class ApplicationUserGymRepository : IApplicationUserGymRepository
    {
        private readonly ApplicationDbContext db;

        public ApplicationUserGymRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<ApplicationUserGymClass> GetAttending(int? id, string userId)
        {
            return await db.AttendingMember.FindAsync(userId, id);
        }
        
        public async Task<IEnumerable<GymClass>> GetBookingsAsync(string userId)
        {
            return await db.AttendingMember
                .Include(g => g.GymClass)
                .ThenInclude(g => g.AttendingMembers)
                .IgnoreQueryFilters()
                .Where(u => u.ApplicationUserID == userId)
                .Select(a => a.GymClass).ToListAsync();
                
        }

        public void Add(ApplicationUserGymClass attending)
        {
            db.Add(attending);
        }

        public void Remove(ApplicationUserGymClass attending)
        {
            db.Remove(attending);
        }
    }
}
