﻿using HotelReservation.Command;
using HotelReservation.Models;
using HotelReservation.Services;
using HotelReservation.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelReservation.ViewModel
{
    public class MakeReservationViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        int roomNumber; //book phong nao : tham chieu den bang danh muc phong
        int floorNumber;

        DateTime startDate; //bat dau luc nao
        DateTime endDate; //ket thuc luc nao
        string userName;//who make a reservation

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; OnPropertyChanged(nameof(StartDate)); }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {

                endDate = value;
                OnPropertyChanged(nameof(EndDate));

                ClearError(nameof(EndDate));
                if (EndDate < StartDate)
                {
                    string endDateError = "The EndDate can not < the Start Date";
                    AddError(nameof(EndDate), endDateError);
                    OnErrorsChanged(nameof(EndDate));
                }
            }
        }

        private void ClearError(string propName)
        {
            _propToErrorDicionary.Remove(propName);
        }

        private void AddError(string propName, string propError)
        {
            if (!_propToErrorDicionary.Keys.Contains(propName))
            {
                _propToErrorDicionary.Add(propName, new List<string>());
            }
            _propToErrorDicionary[propName].Add(propError);
            OnErrorsChanged(propName);
        }

        private void OnErrorsChanged(string propName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(EndDate)));
        }

        public int RoomNumber
        {
            get
            {
                return roomNumber;
            }
            set
            {
                roomNumber = value;
                OnPropertyChanged(nameof(RoomNumber));
            }
        }

        public int FloorNumber
        {
            get
            {
                return floorNumber;
            }
            set
            {
                floorNumber = value;
                OnPropertyChanged(nameof(FloorNumber));
            }
        }

        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public bool HasErrors => _propToErrorDicionary.Any();

        NavigationService<ReservationListingViewModel> _reservationViewNavigationService;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private readonly Dictionary<string, List<string>> _propToErrorDicionary;

        public IEnumerable GetError(string propName)
        {
            return _propToErrorDicionary.GetValueOrDefault(propName, new List<string>());
        }
        public MakeReservationViewModel(HotelStore hotelStore,

            NavigationService<ReservationListingViewModel> reservationViewNavigationService)
        {
            _reservationViewNavigationService = reservationViewNavigationService;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;

            SubmitCommand = new MakeReservationCommand(hotelStore, this, _reservationViewNavigationService);
            CancelCommand = new NavigateCommand<ReservationListingViewModel>(_reservationViewNavigationService);
            Title = "Make Reservation";
            //SubmitCommand.CanExecuteChanged += SubmitCommand_CanExecuteChanged; 
        }

        private void SubmitCommand_CanExecuteChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public IEnumerable GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
