using System;
using System.Collections.Generic;

#nullable disable

namespace HotelApp.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
