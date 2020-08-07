using HotelApp.BLL;
using HotelApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace HotelApp
{
    
    public partial class MainWindow : Window
    {
        public IEnumerable<InputModel> Reservations { get; set; }

        private readonly IBusinessLogic businessLogic;
        public MainWindow(IBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
            InitializeComponent();
            InitButtons();
            Loaded += OnLoaded;
        }

        private void InitButtons()
        {
            CreateResBtn.Click += CreateOnClick;
            RemoveResBtn.Click += RemoveOnClick;
            UpdateResBtn.Click += UpdateOnClick;
        }
        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            await SetData();
            
        }
        
        private async void RemoveOnClick(object sender, RoutedEventArgs e)
        {
            var item = (InputModel)DataTable.SelectedItem;
            if (item != null)
            {
                await businessLogic.RemoveReservation(item.ReservationID);
                await SetData();
            }
        }
        private void CreateOnClick(object sender, RoutedEventArgs e)
        {
            CreationWindow popup = new CreationWindow(businessLogic);
            popup.ShowDialog();
            popup.Closing += PopupClosed;
        }
        private async void PopupClosed(object sender, EventArgs e)
        {
            await SetData();
        }
        private void UpdateOnClick(object sender, RoutedEventArgs e)
        {
            var item = (InputModel)DataTable.SelectedItem;
            if (item != null)
            {
                CreationWindow popup = new CreationWindow(businessLogic, (InputModel)DataTable.SelectedItem);
                popup.ShowDialog();
                popup.Closing += PopupClosed;
            }
        }
        private async Task SetData()
        {
            var reservations = await businessLogic.GetReservations();
            DataTable.ItemsSource = reservations.Select(a => new InputModel(a));
            SetColumnNames();
        }

        private void SetColumnNames()
        {
            DataTable.Columns[0].Visibility = Visibility.Collapsed;
            DataTable.Columns[1].Header = "Początek";
            DataTable.Columns[2].Header = "Koniec";
            DataTable.Columns[3].Header = "Goście";
            DataTable.Columns[4].Header = "Numer p.";
            DataTable.Columns[5].Header = "Piętro";
            DataTable.Columns[6].Header = "Typ pokoju";
            DataTable.Columns[7].Header = "Metraż";
            DataTable.Columns[8].Header = "Liczba miejsc";
            DataTable.Columns[9].Header = "Cena/noc";
            DataTable.Columns[10].Header = "Imię";
            DataTable.Columns[11].Header = "Nazwisko";
            DataTable.Columns[12].Header = "Email";
            DataTable.Columns[13].Header = "Telefon";
        }
    }
}
