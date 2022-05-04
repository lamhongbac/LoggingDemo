using HotelReservation.DbContexts;
using HotelReservation.DTOs;
using HotelReservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Services.ReservationProvider
{
    public class DatabaseReservationCreator : IReservationCreator
    {
        private readonly ReserRoomDbContextFactory reserRoomDbContextFactory;
       public DatabaseReservationCreator(ReserRoomDbContextFactory _reserRoomDbContextFactory)
        {
            reserRoomDbContextFactory = _reserRoomDbContextFactory;
        }
        public async Task CreateReservation(Reservation reservation)
        {
            using (ReserRoomDbContext reserRoomDbContext = reserRoomDbContextFactory.CreateDbContext())
            {
                ReservationDTO reservationDTO = ToReservationDTO(reservation);
                reserRoomDbContext.Add(reservationDTO);
                await reserRoomDbContext.SaveChangesAsync();
            }
        }

        private ReservationDTO ToReservationDTO(Reservation reservation)
        {
            return new ReservationDTO()
            {
                Id = Guid.NewGuid(),
                EndTime = reservation.EndTime,
                FloorNumber = reservation.Room.FloorNumber,
                Number = reservation.Room.Number,
                StartTime = reservation.StartTime,
                UserName = reservation.UserName,


            };

        }
    }
}
