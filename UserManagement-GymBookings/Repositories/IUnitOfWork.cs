using System.Threading.Tasks;

namespace UserManagement_GymBookings.Repositories
{
    public interface IUnitOfWork
    {
        IApplicationUserGymRepository AppUserRepo { get; set; }
        IGymClassRepository GymClassRepository { get; set; }

        Task CompleteAsync();
    }
}