using System;
using System.Collections.Generic;

#nullable disable

namespace HotelApp.Models
{
    public partial class CustomersByReservationTime
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? Reservations { get; set; }
        public double? AverageTime { get; set; }
        public int? MostReserved { get; set; }
    }
}
