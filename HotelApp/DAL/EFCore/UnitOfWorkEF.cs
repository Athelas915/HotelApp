using HotelApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HotelApp.DAL.EFCore
{
    public class UnitOfWorkEF : IUnitOfWork
    {
        private readonly HotelDBContext dbContext;

        public UnitOfWorkEF(HotelDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Save()
        {
            await dbContext.SaveChangesAsync();
        }

        public DbSet<T> Set<T>() where T : class
        {
            return dbContext.Set<T>();
        }
    }
}
