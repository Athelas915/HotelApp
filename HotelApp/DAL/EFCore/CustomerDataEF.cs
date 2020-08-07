using HotelApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.DAL.EFCore
{
    public class CustomerDataEF : DataEF<Customer>, ICustomerData
    {
        public CustomerDataEF(IUnitOfWork uow) : base(uow) { }

        public async Task<Customer> FindByEmail(string email)
            =>
            (await Get(filter: a => a.Email == email))
            .FirstOrDefault();
    }
}
