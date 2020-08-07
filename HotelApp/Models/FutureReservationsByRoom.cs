using System;
using System.Collections.Generic;

#nullable disable

namespace HotelApp.Models
{
    public partial class FutureReservationsByRoom
    {
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public int FloorNumber { get; set; }
        public int ReservationCount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
