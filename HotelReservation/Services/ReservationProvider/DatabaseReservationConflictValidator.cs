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
    public class DatabaseReservationConflictValidator : IReservationConflictValidator
    {
        private readonly ReserRoomDbContextFactory reserRoomDbContextFactory;
        public DatabaseReservationConflictValidator(ReserRoomDbContextFactory _reserRoomDbContextFactory)
        {
            reserRoomDbContextFactory = _reserRoomDbContextFactory;
        }
        public async Task<Reservation> GetConflictReservation(Reservation reservation)
        {
            using (ReserRoomDbContext dbContext = reserRoomDbContextFactory.CreateDbContext())
            {
                ReservationDTO dto = await dbContext.Reservations.
                      Where(r => r.Number == reservation.Room.Number)
                      .Where(r => r.FloorNumber == reservation.Room.FloorNumber)
                      .Where(r => r.EndTime > reservation.StartTime)
                      .Where(r => r.StartTime < reservation.EndTime)
                    .FirstOrDefaultAsync();
                if (dto == null)
                    return null;
                return ToReservation(dto);
            }
        }

        private static Reservation ToReservation(ReservationDTO dto)
        {
            return new Reservation(new RoomID(dto.Number, dto.FloorNumber), dto.StartTime, dto.EndTime, dto.UserName);
        }
    }
}
