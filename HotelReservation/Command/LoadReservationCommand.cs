using HotelReservation.Models;
using HotelReservation.Services.ReservationProvider;
using HotelReservation.Stores;
using HotelReservation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelReservation.Command
{
    /// <summary>
    /// la 1 command trực thuộc ListViewModel
    /// Thuc hien viec load data tu CSDL vao ListViewModel
    /// thong qua hàm tĩnh  static ReservationListingViewModel LoadViewModel
    /// ==>viewModel.LoadReservationCommand.Execute
    /// </summary>
    public class LoadReservationCommand : AsyncCommandBase
    {
        
        private readonly HotelStore _hotelStore;
        private readonly ReservationListingViewModel _viewModel;
       public LoadReservationCommand(ReservationListingViewModel viewModel, HotelStore hotelStore)
        {
            _hotelStore = hotelStore; 
            _viewModel = viewModel;
        }
        /// <summary>
        /// thuc chat load command van la su dung lai ham UpdateReservation ben trong ReservationListViewModel
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
             
                _viewModel.IsLoading=true;
                await Task.Delay(3000);
                await _hotelStore.Load();
                _viewModel.UpdateReservation(_hotelStore.Reservations);
            }
            catch(Exception)
            {
                MessageBox.Show("Failed to load reservation", "Loading Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _viewModel.IsLoading = false;
        }
    }
}
