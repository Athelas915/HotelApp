using HotelApp.BLL;
using HotelApp.Models;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelApp
{
    /// <summary>
    /// Interaction logic for CreationWindow.xaml
    /// </summary>
    public partial class CreationWindow : Window
    {
        private bool isNewRecord;
        public InputModel Input { get; set; }
        public IList<string> Errors { get; set; }

        private readonly IBusinessLogic businessLogic;
        public CreationWindow(IBusinessLogic businessLogic, InputModel input)
        {
            Input = new InputModel(input);
            Errors = new List<string>();
            this.businessLogic = businessLogic;
            InitializeComponent();
            InitButtons();
            DataContext = this;
            isNewRecord = false;
            CreateResBtn.Content = "Aktualizuj rezerwację";
        }
        public CreationWindow(IBusinessLogic businessLogic)
        {
            Input = new InputModel();
            Errors = new List<string>();
            this.businessLogic = businessLogic;
            InitializeComponent();
            InitButtons();
            DataContext = this;
            isNewRecord = true;
            CreateResBtn.Content = "Dodaj rezerwację";
        }
        private void InitButtons()
        {
            CloseBtn.Click += CloseOnClick;
            CreateResBtn.Click += CreateOnClick;
        }
        private void CloseOnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private async void CreateOnClick(object sender, RoutedEventArgs e)
        {
            Errors = new List<string>();
            var cust = new Customer()
            {
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                Email = Input.Email,
                Phone = Input.Phone
            };
            var room = await businessLogic.GetRoomByNumber(Input.RoomNumber);
            if (room == null) 
            {
                Errors.Add("Podano zły numer pokoju.");
                ErrorTable.ItemsSource = Errors;
                return; 
            }

            var res = new Reservation()
            {
                FromDate = DateTime.Parse(Input.FromDate),
                ToDate = DateTime.Parse(Input.ToDate),
                NumberOfGuests = Input.NumberOfGuests,
                Customer = cust,
                Room = room
            };
            var result = isNewRecord ? await businessLogic.AddReservation(res) : await businessLogic.UpdateReservation(res);
            if (result.Succeeded)
            {
                this.Close();
                return;
            }
            foreach(var err in result.ErrorMessages)
            {
                Errors.Add(err);
            }
            ErrorTable.ItemsSource = Errors;
            return;
        }
    }
}
