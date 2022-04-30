using HotelReservation.Command;
using HotelReservation.Models;
using HotelReservation.Services;
using HotelReservation.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelReservation.ViewModel
{
    public class MakeReservationViewModel : ViewModelBase
    {
        int roomNumber; //book phong nao : tham chieu den bang danh muc phong
        int floorNumber;

        DateTime startTime; //bat dau luc nao
        DateTime endTime; //ket thuc luc nao
        string userName;//who make a reservation

        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; OnPropertyChanged(nameof(StartTime)); }
        }

        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; OnPropertyChanged(nameof(EndTime)); }
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
        NavigationService _reservationViewNavigationService;
        public MakeReservationViewModel(Hotel hotel,
           
            NavigationService reservationViewNavigationService)
        {
            _reservationViewNavigationService = reservationViewNavigationService;
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;

               SubmitCommand = new MakeReservationCommand(hotel, this, _reservationViewNavigationService);
            CancelCommand = new NavigateCommand(_reservationViewNavigationService);
            Title = "Make Reservation";
        }

      
    }
}
