using System;
using System.Collections.Generic;

#nullable disable

namespace HotelApp.Models
{
    public partial class Room
    {
        public Room()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public int FloorNumber { get; set; }
        public string RoomType { get; set; }
        public int Area { get; set; }
        public int Capacity { get; set; }
        public int PricePerNight { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
