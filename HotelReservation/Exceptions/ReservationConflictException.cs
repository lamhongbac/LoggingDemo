using HotelReservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Exceptions
{
    public class ReservationConflictException : Exception
    {
        public Reservation ExistReservation { get; }
        public Reservation ConflictReservation { get; }
        public ReservationConflictException(Reservation existReservation, Reservation conflictReservation)
        {
            ConflictReservation = conflictReservation;
            ExistReservation = existReservation;
        }

        public ReservationConflictException(Reservation existReservation, Reservation conflictReservation,string message) : base(message)
        {
            ConflictReservation = conflictReservation;
            ExistReservation = existReservation;

        }

        public ReservationConflictException(Reservation existReservation, Reservation conflictReservation,string message, Exception innerException) : base(message, innerException)
        {
            ConflictReservation = conflictReservation;
            ExistReservation = existReservation;

        }

        protected ReservationConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
