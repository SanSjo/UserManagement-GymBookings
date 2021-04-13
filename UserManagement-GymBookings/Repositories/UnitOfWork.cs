using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement_GymBookings.Data;

namespace UserManagement_GymBookings.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;
        public IGymClassRepository GymClassRepository { get; set; }
        public IApplicationUserGymRepository AppUserRepo { get; set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
            GymClassRepository = new GymClassRepository(db);
            AppUserRepo = new ApplicationUserGymRepository(db);
        }

        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
