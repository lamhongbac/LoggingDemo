using HotelReservation.DbContexts;
using HotelReservation.Exceptions;
using HotelReservation.HostBuilders;
using HotelReservation.Models;
using HotelReservation.Services;
using HotelReservation.Services.ReservationProvider;
using HotelReservation.Stores;
using HotelReservation.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        //private const string CONNECTION_STRING = "Data Source = ReserRoom.Db";
        //NavigationStore _navigationStores;
        //IReservationCreator reservationCreator;
        //IReservationProvider reservationProvider;
        //IReservationConflictValidator reservationConflict;
        //Hotel _hotel;
        //HotelStore _hotelStore;
        private readonly IHost _host;
        //ReserRoomDbContextFactory dbContextFactory;
        public App()
        {
           _host= Host.CreateDefaultBuilder()
                .AddViewModel()
                .ConfigureServices((hostContext,service)=>
            {
              string connectionString=  hostContext.Configuration.GetConnectionString("Default");
                service.AddSingleton(new ReserRoomDbContextFactory(connectionString));
                service.AddSingleton<IReservationCreator, DatabaseReservationCreator>();
                service.AddSingleton<IReservationProvider, DataBaseReservationProvider>();
                service.AddSingleton<IReservationConflictValidator, DatabaseReservationConflictValidator>();
                //Reservation Book co constructor la cac interface da add vao DI o tren, vi the kg can khai bao
                //thong qua get requireservice

                service.AddTransient<ReservationBook>();
                
                service.AddSingleton(s=> new Hotel("Lam's Hotel", s.GetRequiredService<ReservationBook>()));
                
                

                //thong thuong se lam nhu sau
                //==>service.AddSingleton(s=>new HotelStore(s.GetRequiredService<Hotel>()));
                //Tuy nhien Hotel store co constructor cần para là Hotel, va da dc add vao trong DI
                //==> chi can lam the nay

                //HotelStore(hotel)
                service.AddSingleton<HotelStore>();

                service.AddSingleton<NavigationStore>();
                // MainViewModel(NavigationStore);
                service.AddSingleton<MainViewModel>();

                //service.AddSingleton(x => new MainWindow(x.GetRequiredService<Hotel>()));
                service.AddSingleton(x => new MainWindow() 
                { 
                    DataContext = x.GetRequiredService<MainViewModel>() 
                }); 

            }).Build();

            // dbContextFactory = new ReserRoomDbContextFactory(CONNECTION_STRING);
            ////ReserRoomDbContext dbContext = dbContextFactory.CreateDbContext();
            //reservationProvider = new DataBaseReservationProvider(dbContextFactory);
            //reservationCreator = new DatabaseReservationCreator(dbContextFactory);
            //reservationConflict = new DatabaseReservationConflictValidator(dbContextFactory);
            //ReservationBook reservationBook = new ReservationBook(reservationCreator, reservationProvider, reservationConflict);

            //_hotel = new Hotel("Lam's Hotel", reservationBook);
            //_hotelStore = new HotelStore(_hotel);
            //_navigationStores = new NavigationStore();

        }

       

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            ReserRoomDbContextFactory dbContextFactory = _host.Services.GetRequiredService<ReserRoomDbContextFactory>();
            //DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(CONNECTION_STRING).Options;
            using (ReserRoomDbContext dbContext = dbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }
            NavigationService<ReservationListingViewModel> navigationService =
                _host.Services.GetRequiredService<NavigationService<ReservationListingViewModel>>();
            navigationService.Navigate();

            //NavigationStore _navigationStores = _host.Services.GetRequiredService<NavigationStore>();

            //_navigationStores.CurrentViewModel = CreateReservationListingViewModel();// ReservationListingViewModel();

            MainWindow mainWindown = _host.Services.GetRequiredService<MainWindow>(); //new MainWindow(_hotel)
                                                                                      //{
            //mainWindown.DataContext = _host.Services.GetRequiredService<MainViewModel>();// new MainViewModel(_navigationStores)
            //});
            //_host.Services.GetRequiredService<MainWindow>();

            mainWindown.Show(); 
            base.OnStartup(e);
        }
        //private MakeReservationViewModel CreateMakeReservationViewModel()
        //{
        //    return new MakeReservationViewModel(_hotelStore,
        //        new Services.NavigationService( _navigationStores, 
        //        CreateReservationListingViewModel));
        //}

        //private ReservationListingViewModel CreateReservationListingViewModel()
        //{
        //    return  ReservationListingViewModel.LoadViewModel(_hotelStore, 
        //        CreateMakeReservationViewModel(),
        //        new Services.NavigationService(_navigationStores,
        //        CreateMakeReservationViewModel));
        //}
    }
}
