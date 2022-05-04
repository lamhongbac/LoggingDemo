using HotelReservation.Exceptions;
using HotelReservation.Models;
using HotelReservation.Services;
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
    /// command attached vao button submit cua view MakeReservation
    /// thuc hien hanh dong tao ra 1 record trong CSDL
    /// UC1: quay ve view ListView, Update ListView with new data
    /// UC2: 2 view cung o tren 1 view cha ->update ListView with new data
    /// </summary>
    public class MakeReservationCommand : AsyncCommandBase
    {
        HotelStore _hotelStore;
        MakeReservationViewModel _viewModel;
        NavigationService<ReservationListingViewModel> _reservationViewNavigationService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hotelStore">HotelStore: noi cung cap data cho view</param>
        /// <param name="viewModel">hosting view (view contain command button)</param>
        /// <param name="reservationViewNavigationService">UC1:dung de chuyen huong ve list view</param>
        public MakeReservationCommand(HotelStore hotelStore,MakeReservationViewModel viewModel,
            NavigationService<ReservationListingViewModel> reservationViewNavigationService)
        {
            _hotelStore = hotelStore;
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
            _reservationViewNavigationService = reservationViewNavigationService; ;
        }
        /// <summary>
        /// Usage:
        /// thuc hien ham CanExecute, khi propty of view model change
        /// nham dam bao realtime enable/disable button submit tren view
        /// 
        /// khi property change =>OnExecutedChanged isfire,
        /// =>CanExecuteChanged event is fired
        /// =>hosting viewmodel (in this case la MakeReservationViewModel)
        /// se nhan dc signal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName==nameof(MakeReservationViewModel.UserName) ||
                e.PropertyName == nameof(MakeReservationViewModel.FloorNumber)||
                e.PropertyName == nameof(MakeReservationViewModel.RoomNumber))
            {
                OnExecutedChanged(); //thuc hien kiem tra moi khi prop change value
            }
        }
        /// <summary>
        /// ham xac dinh enable or disable cua button
        /// khi co thay doi tren view-> viewmodel-> can thuc hien thong bao cho command object
        /// command object se lang nghe (_viewModel_PropertyChanged)va thuc hien kiem tra (OnExecutedChanged)
        /// sau do ham CanExecute --> tra ve true/false tuong duong enable or disable cua button
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(_viewModel.UserName)
                && _viewModel.FloorNumber>0 &&  _viewModel.RoomNumber>0 
                && base.CanExecute(parameter);
        }
       

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                Reservation reservation = new Reservation(new RoomID(_viewModel.RoomNumber, _viewModel.FloorNumber),
                    _viewModel.StartTime, _viewModel.EndTime, _viewModel.UserName);
                await _hotelStore.MadeReservation(reservation);
                MessageBox.Show("this room is booked success", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                _reservationViewNavigationService.Navigate();
            }
            catch (ReservationConflictException)
            {
                MessageBox.Show("This room is already taken", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                //throw;
            }
            catch(Exception)
            {
                MessageBox.Show("Failed to create reservation", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
