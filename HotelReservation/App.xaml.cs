using HotelReservation.Exceptions;
using HotelReservation.Models;
using HotelReservation.Stores;
using HotelReservation.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HotelReservation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        NavigationStore _navigationStores;
        Hotel _hotel;
        public App()
        {
             _hotel = new Hotel("LamHongBac");
            _navigationStores = new NavigationStore();

        }
        protected override void OnStartup(StartupEventArgs e)
        {

            //Hotel hotel = new Hotel("Demo");
            //try
            //{
            //    hotel.CreateReservation(
            //       new Reservation(new RoomID(1, 2), new DateTime(2022, 4, 23), new DateTime(2022, 4, 24), 
            //       "Bac", "Nam")); 

            //    hotel.CreateReservation(
            //       new Reservation(new RoomID(1, 2), new DateTime(2022, 4, 24), 
            //       new DateTime(2022, 4, 25), "Bac", "Nam")) ;
            //}
            //catch(ReservationConflictException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //IEnumerable<Reservation> reservations = hotel.GetUserReservation("Demo");

            _navigationStores.CurrentViewModel = new ReservationListingViewModel();

            MainWindow mainWindown = new MainWindow(_hotel)
            {
                DataContext = new MainViewModel(_navigationStores)
            };
            mainWindown.Show(); 
            base.OnStartup(e);
        }
    }
}
