using HotelReservation.DbContexts;
using HotelReservation.Exceptions;
using HotelReservation.Models;
using HotelReservation.Services.ReservationProvider;
using HotelReservation.Stores;
using HotelReservation.ViewModel;
using Microsoft.EntityFrameworkCore;
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
        private const string CONNECTION_STRING = "Data Source = ReserRoom.Db";
        NavigationStore _navigationStores;
        IReservationCreator reservationCreator;
        IReservationProvider reservationProvider;
        IReservationConflictValidator reservationConflict;
        Hotel _hotel;
        public App()
        {
            ReserRoomDbContextFactory dbContextFactory = new ReserRoomDbContextFactory(CONNECTION_STRING);
            //ReserRoomDbContext dbContext = dbContextFactory.CreateDbContext();
            reservationProvider = new DataBaseReservationProvider(dbContextFactory);
            reservationCreator = new DatabaseReservationCreator(dbContextFactory);
            reservationConflict = new DatabaseReservationConflictValidator(dbContextFactory);
            ReservationBook reservationBook = new ReservationBook(reservationCreator, reservationProvider, reservationConflict);

            _hotel = new Hotel("Lam's Hotel", reservationBook);
            _navigationStores = new NavigationStore();

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(CONNECTION_STRING).Options;
            using (DbContext dbContext = new ReserRoomDbContext(options))
            {
                dbContext.Database.Migrate();
            }
            
            _navigationStores.CurrentViewModel = CreateReservationListingViewModel();// ReservationListingViewModel();

            MainWindow mainWindown = new MainWindow(_hotel)
            {
                DataContext = new MainViewModel(_navigationStores)
            };
            mainWindown.Show(); 
            base.OnStartup(e);
        }
        private MakeReservationViewModel CreateMakeReservationViewModel()
        {
            return new MakeReservationViewModel(_hotel,new Services.NavigationService( _navigationStores, CreateReservationListingViewModel));
        }

        private ReservationListingViewModel CreateReservationListingViewModel()
        {
            return  ReservationListingViewModel.LoadViewModel(_hotel,
                new Services.NavigationService(_navigationStores,CreateMakeReservationViewModel));
        }
    }
}
