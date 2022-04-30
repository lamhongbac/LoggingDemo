using HotelReservation.DbContexts;
using HotelReservation.DTOs;
using HotelReservation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Services.ReservationProvider
{
    public class DataBaseReservationProvider : IReservationProvider
    {
        private readonly ReserRoomDbContextFactory _dbContextFactory;
        public DataBaseReservationProvider(ReserRoomDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            using(ReserRoomDbContext reserRoomDbContext = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ReservationDTO> reservationDTOs= await reserRoomDbContext.Reservations.ToListAsync();
                IEnumerable<Reservation> reservations = 
                    reservationDTOs.Select(r =>ToReservation(r));

                return reservations;
            }
        }
        private Reservation ToReservation(ReservationDTO r)
        {
           return new Reservation(new RoomID(r.Number, r.FloorNumber), r.StartTime, r.EndTime, r.UserName);
        }
    }
}
