using HotelReservation.Services;
using HotelReservation.Stores;
using HotelReservation.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.HostBuilders
{
  public static class AddViewModelHostBuilderExtentions
    {
        public static IHostBuilder AddViewModel(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(service =>
            {
                service.AddTransient<MakeReservationViewModel>();
                service.AddSingleton<Func<MakeReservationViewModel>>(s => () => s.GetRequiredService<MakeReservationViewModel>());
                service.AddSingleton<NavigationService<MakeReservationViewModel>>();

                service.AddTransient(s => CreateReservationListVM(s));
                service.AddSingleton<Func<ReservationListingViewModel>>(s => () => s.GetRequiredService<ReservationListingViewModel>());

                //NavigationService(NavigationStore _navigationStore, Func<TViewModel> createViewModel)
                service.AddSingleton<NavigationService<ReservationListingViewModel>>();
            });
            return hostBuilder;
        }
        private static ReservationListingViewModel CreateReservationListVM(IServiceProvider s)
        {
            HotelStore hotelStore = s.GetRequiredService<HotelStore>();
            MakeReservationViewModel makeReservationViewModels = s.GetRequiredService<MakeReservationViewModel>();
            NavigationService<ReservationListingViewModel> makeReservationNavigationService =
                s.GetRequiredService<NavigationService<ReservationListingViewModel>>();

            return ReservationListingViewModel.LoadViewModel(hotelStore, makeReservationViewModels, makeReservationNavigationService);
        }
    }
}
