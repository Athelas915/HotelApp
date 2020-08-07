using HotelApp.Models;
using System.Threading.Tasks;

namespace HotelApp.DAL
{
    public interface ICustomerData : IData<Customer>
    {
        Task<Customer> FindByEmail(string email);
    }
}
