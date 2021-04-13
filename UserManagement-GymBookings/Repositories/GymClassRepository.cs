using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement_GymBookings.Data;
using UserManagement_GymBookings.Models;

namespace UserManagement_GymBookings.Repositories
{
    public class GymClassRepository : IGymClassRepository
    {
        private readonly ApplicationDbContext db;

        public GymClassRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<GymClass> GetAsync(int? id)
        {
            return await db.GymClass
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<GymClass>> GetAllAsync()
        {
            return await db.GymClass
                .ToListAsync();
        }

        public async Task<IEnumerable<GymClass>> GetWithBookingsAsync()
        {
            return await db.GymClass
                .Include(b => b.AttendingMembers).ToListAsync();
        }


        public async Task<IEnumerable<GymClass>> GetHistoryAsync()
        {
            return await db.GymClass
                .Include(g => g.AttendingMembers).IgnoreQueryFilters().Where(b => b.StartTime < DateTime.Now).ToListAsync();
        }

        public void Add(GymClass gymclass)
        {
            db.Add(gymclass);
        }
        
        public void Update(GymClass gymclass)
        {
            db.Update(gymclass);
        }
        
        public void Remove(GymClass gymclass)
        {
            db.Remove(gymclass);
        }
        
        public bool Any(int id)
        {
            return db.GymClass.Include(a => a.AttendingMembers).Any(g => g.Id == id);
        }
        
        public async Task<GymClass> FindAsync(int? id)
        {
            return await db.GymClass.FindAsync(id);
        }


    }
}
