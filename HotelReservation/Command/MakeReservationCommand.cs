using HotelReservation.Exceptions;
using HotelReservation.Models;
using HotelReservation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelReservation.Command
{
    public class MakeReservationCommand : CommandBase
    {
        Hotel _hotel;
        MakeReservationViewModel _viewModel;
        public MakeReservationCommand(Hotel hotel,MakeReservationViewModel viewModel )
        {
            _hotel = hotel;
            _viewModel = viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }
        /// <summary>
        /// khi property change this event is fired
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName==nameof(MakeReservationViewModel.UserName) ||
                e.PropertyName == nameof(MakeReservationViewModel.FloorNumber)||
                e.PropertyName == nameof(MakeReservationViewModel.UserName))
            {
                OnExecutedChanged(); //thuc hien kiem tra moi khi prop change value
            }
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(_viewModel.UserName)
                && _viewModel.FloorNumber>0 &&  _viewModel.RoomNumber>0 
                && base.CanExecute(parameter);
        }
        public override void Execute(object parameter)
        {
            try
            {
                Reservation reservation = new Reservation(new RoomID(_viewModel.RoomNumber, _viewModel.FloorNumber),
                    _viewModel.StartTime, _viewModel.EndTime, _viewModel.UserName, "Cust1");
                _hotel.CreateReservation(reservation);
                MessageBox.Show("this room is booked success","Information",MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (ReservationConflictException)
            {
                MessageBox.Show("This room is taken", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                throw;
            }
        }
    }
}
