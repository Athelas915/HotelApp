using HotelApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.DAL.EFCore
{
    public class ReservationDataEF : DataEF<Reservation>, IReservationData
    {
        public ReservationDataEF(IUnitOfWork uow) : base(uow) { }
    }
}
