using HotelApp.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.BLL
{
    public interface IBusinessLogic
    {
        public Task<IEnumerable<Reservation>> GetReservations();
        public Task<IBLLActionResult> AddReservation(Reservation res);
        public Task<IBLLActionResult> UpdateReservation(Reservation res);
        public Task RemoveReservation(Reservation res);
        public Task RemoveReservation(int resId);
        public Task<IEnumerable<Room>> GetFreeRooms(DateTime from, DateTime to);
        public Task<Room> GetRoomByNumber(int num);

    }
}
