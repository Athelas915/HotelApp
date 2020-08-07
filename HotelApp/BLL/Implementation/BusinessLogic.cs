using HotelApp.DAL;
using HotelApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.BLL.Implementation
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly IRoomData roomData;
        private readonly ICustomerData customerData;
        private readonly IReservationData reservationData;

        public BusinessLogic(IRoomData roomData, ICustomerData customerData, IReservationData reservationData)
        {
            this.roomData = roomData;
            this.customerData = customerData;
            this.reservationData = reservationData;
        }

        private IBLLActionResult ValidateReservation(Reservation r)
        {
            var result = new BLLActionResult();

            if (r.FromDate >= r.ToDate)
            {
                result.AddError("Niepoprawne daty.");
            }

            return result;
        }
        private IBLLActionResult ValidateCustomer(Customer c)
        {
            var result = new BLLActionResult();

            if (c == null)
            {
                result.AddError("Nie podano danych gościa.");
            }
            else
            {
                if (c.FirstName == "")
                {
                    result.AddError("Nie podano Imienia.");
                }
                if (c.LastName == "")
                {
                    result.AddError("Nie podano Nazwiska.");
                }
                if (c.Email == "")
                {
                    result.AddError("Nie podano Emaila.");
                }
                if (c.Phone == "")
                {
                    result.AddError("Nie podano Telefonu.");
                }
            }

            return result;
        }
        public async Task<Room> GetRoomByNumber(int num)
            => (await roomData
            .Get(filter: a => a.RoomNumber == num))
            .FirstOrDefault();

        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            var result = await reservationData.Get();

            foreach (var res in result)
            {
                res.Customer = await customerData.GetById(res.CustomerId);
                res.Room = await roomData.GetById(res.RoomId);
            }

            return result;
        }
        public async Task<IBLLActionResult> AddReservation(Reservation res)
        {
            var result = ValidateReservation(res);

            var freeRooms = await GetFreeRooms(res.FromDate, res.ToDate);
            if (!freeRooms.Contains(res.Room))
            {
                result.AddError("Pokój zajęty w wyznaczonym terminie.");
            }
            if (res.FromDate < DateTime.Now)
            {
                result.AddError("Data początku rezerwacji już minęła.");
            }

            //Checking if customer is already in our database.
            var cust = await customerData.FindByEmail(res.Customer.Email);
            if (cust != null)
            {

                res.Customer = cust;
                res.CustomerId = cust.CustomerId;
            }

            var resCus = ValidateCustomer(res.Customer);
            foreach (var err in resCus.ErrorMessages)
            {
                result.AddError(err);
            }
            if (result.Succeeded)
            {
                try
                {
                    reservationData.Create(res);
                    await reservationData.CommitAllRepos();
                }
                catch (DbUpdateException)
                {
                    result.AddError("Błędne dane.");
                }
            }
            return result;
        }

        public async Task<IEnumerable<Room>> GetFreeRooms(DateTime from, DateTime to)
        {
            var reservedRooms = (await reservationData.Get(filter: a => a.ToDate > from && a.FromDate < to)).Select(b => b.RoomId);

            return await roomData.Get(filter: c => !reservedRooms.Contains(c.RoomId));
        }

        public async Task RemoveReservation(Reservation res)
        {
            reservationData.Delete(res);
            await reservationData.CommitAllRepos();
        }
        public async Task RemoveReservation(int resId)
        {
            await reservationData.Delete(resId);
            await reservationData.CommitAllRepos();
        }

        public async Task<IBLLActionResult> UpdateReservation(Reservation res)
        {
            var result = ValidateReservation(res);

            IList<Room> freeRooms = (await GetFreeRooms(res.FromDate, res.ToDate)).ToList();
            freeRooms.Add(res.Room);
            if (!freeRooms.Contains(res.Room))
            {
                result.AddError("Pokój zajęty w wyznaczonym terminie.");
            }
            if (res.ToDate < DateTime.Now)
            {
                result.AddError("Zarezerwowany poby już się odbył.");
            }

            //Checking if customer is already in our database.
            var cust = await customerData.FindByEmail(res.Customer.Email);
            if (cust != null)
            {
                res.Customer = cust;
                res.CustomerId = cust.CustomerId;
            }
            else
            {
                var resCus = ValidateCustomer(res.Customer);
                foreach (var err in resCus.ErrorMessages)
                {
                    result.AddError(err);
                }
            }

            if (result.Succeeded)
            {
                try
                {
                    reservationData.Update(res);
                    await reservationData.CommitAllRepos();
                }
                catch (DbUpdateException)
                {
                    result.AddError("Błędne dane.");
                }
            }
            return result;
        }
    }
}
