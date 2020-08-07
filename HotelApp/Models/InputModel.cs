using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HotelApp.Models
{
    public class InputModel : INotifyPropertyChanged
    {
        private readonly Reservation res;
        public InputModel()
        {
            res = new Reservation();
            res.Customer = new Customer();
            res.Room = new Room();
            res.FromDate = DateTime.Now;
            res.ToDate = DateTime.Now;
        }
        //Copy constructor
        public InputModel(InputModel other)
        {
            var r = other.GetModel();

            res = new Reservation();
            res.FromDate = r.FromDate;
            res.ToDate = r.ToDate;
            res.NumberOfGuests = r.NumberOfGuests;
            res.ReservationId = r.ReservationId;
            res.Room = new Room();
            res.Room.Area = r.Room.Area;
            res.Room.Capacity = r.Room.Capacity;
            res.Room.FloorNumber = r.Room.FloorNumber;
            res.Room.PricePerNight = r.Room.PricePerNight;
            res.Room.RoomId = r.Room.RoomId;
            res.Room.RoomNumber = r.Room.RoomNumber;
            res.Customer = new Customer();
            res.Customer.FirstName = r.Customer.FirstName;
            res.Customer.LastName = r.Customer.LastName;
            res.Customer.Email = r.Customer.Email;
            res.Customer.Phone = r.Customer.Phone;
        }
        public InputModel(Reservation res)
        {
            this.res = res;
        }
        public int ReservationID
        {
            get => res.ReservationId;
        }
        public string FromDate
        {
            get => res.FromDate.ToShortDateString();
            set
            {
                try
                {
                    res.FromDate = DateTime.Parse(value);
                    NotifyPropertyChanged("FromDate");
                }
                catch (System.FormatException)
                {

                }
            }
        }
        public string ToDate
        {
            get => res.ToDate.ToShortDateString();
            set
            {
                try
                {
                    res.ToDate = DateTime.Parse(value);
                    NotifyPropertyChanged("ToDate");
                }
                catch (System.FormatException)
                {

                }
            }
        }
        public int NumberOfGuests
        {
            get => res.NumberOfGuests;
            set
            {
                res.NumberOfGuests = value;
                NotifyPropertyChanged("NumberOfGuests");
            }
        }
        public int RoomNumber
        {
            get => res.Room.RoomNumber;
            set
            {
                res.Room.RoomNumber = value;
                NotifyPropertyChanged("RoomNumber");
            }
        }
        public int FloorNumber
        {
            get => res.Room.FloorNumber;
            set
            {
                res.Room.FloorNumber = value;
                NotifyPropertyChanged("FloorNumber");
            }
        }
        public string RoomType
        {
            get => res.Room.RoomType;
            set
            {
                res.Room.RoomType = value;
                NotifyPropertyChanged("RoomType");
            }
        }
        public int Area
        {
            get => res.Room.Area;
            set
            {
                res.Room.Area = value;
                NotifyPropertyChanged("Area");
            }
        }
        public int Capacity
        {
            get => res.Room.Capacity;
            set
            {
                res.Room.Capacity = value;
                NotifyPropertyChanged("Capacity");
            }
        }
        public int PricePerNight
        {
            get => res.Room.PricePerNight;
            set
            {
                res.Room.PricePerNight = value;
                NotifyPropertyChanged("PricePerNight");
            }
        }
        public string FirstName
        {
            get => res.Customer.FirstName;
            set
            {
                res.Customer.FirstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get => res.Customer.LastName;
            set
            {
                res.Customer.LastName = value;
                NotifyPropertyChanged("LastName");
            }
        }
        public string Email
        {
            get => res.Customer.Email;
            set
            {
                res.Customer.Email = value;
                NotifyPropertyChanged("Email");
            }
        }
        public string Phone
        {
            get => res.Customer.Phone;
            set
            {
                res.Customer.Phone = value;
                NotifyPropertyChanged("Phone");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        private Reservation GetModel()
        {
            return res;
        }
    }
}
