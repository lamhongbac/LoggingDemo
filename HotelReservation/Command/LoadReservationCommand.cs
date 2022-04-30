using HotelReservation.Models;
using HotelReservation.Services.ReservationProvider;
using HotelReservation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelReservation.Command
{
    public class LoadReservationCommand : AsyncCommandBase
    {
        
        private readonly Hotel _hotel;
        private readonly ReservationListingViewModel _viewModel;
       public LoadReservationCommand(ReservationListingViewModel viewModel, Hotel hotel)
        {
            _hotel = hotel; _viewModel = viewModel;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                IEnumerable<Reservation> reservations = await _hotel.GetAllReservation();
                _viewModel.UpdateReservation(reservations);
            }
            catch(Exception)
            {
                MessageBox.Show("Failed to load reservation", "Loading Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
