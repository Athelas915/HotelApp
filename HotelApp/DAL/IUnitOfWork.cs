using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HotelApp.DAL
{
    public interface IUnitOfWork
    {
        DbSet<T> Set<T>() where T : class;
        Task Save();
    }
}
