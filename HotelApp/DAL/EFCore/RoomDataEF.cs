using HotelApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.DAL.EFCore
{
    public class RoomDataEF : DataEF<Room>, IRoomData
    {
        public RoomDataEF(IUnitOfWork uow) : base(uow) { }
    }
}
