using System;
using System.Collections.Generic;

#nullable disable

namespace HotelApp.Models
{
    public partial class Reservation
    {
        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int NumberOfGuests { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Room Room { get; set; }
    }
}
