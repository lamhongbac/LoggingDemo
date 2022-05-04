using HotelReservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Stores
{
    /// <summary>
    /// doi tuong chua local data thay vi data ben trong CSDL
    /// nguyen ly: cac view deu dung HotelStore de thuc hien action lien quan den data
    /// HotelStore sau do se dung Hotel/ReservBook thong qua database service de thuc hien 
    /// </summary>
  public  class HotelStore
    {
        //local list or on memory list of reservation
        private readonly List<Reservation> _reservations;

        private readonly Hotel _hotel;
        private readonly Lazy<Task> _init;
        public event Action<Reservation> ReservationMade;
        public IEnumerable<Reservation> Reservations => _reservations;
        public HotelStore(Hotel hotel)
        {
            _reservations = new List<Reservation>();
                
            _hotel = hotel;
            //thuc hien ham load data tu csdl
            _init = new Lazy<Task>(initialize);
        }
        /// <summary>
        /// trave data co duoc tu initialize() 
        /// </summary>
        /// <returns></returns>
        public async Task Load()
        {
            await _init.Value;

        }

        private async Task initialize()
        {
            IEnumerable<Reservation> reservations = await _hotel.GetAllReservation();
            _reservations.Clear();
            _reservations.AddRange(reservations);

        }
        /// <summary>
        /// Create reservation command call this
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        public async Task MadeReservation(Reservation reservation)
        {
            try
            {
                await _hotel.CreateReservation(reservation);
            }
            finally
            {
                _reservations.Add(reservation);
                OnReservationMade(reservation);// thong bao cho client biet la da co 1 action (add... xay ra)
            }
        }
        /// <summary>
        /// After submit button click
        /// </summary>
        /// <param name="reservation"></param>
        private void OnReservationMade(Reservation reservation)
        {
            ReservationMade?.Invoke(reservation);
        }
    }
}
