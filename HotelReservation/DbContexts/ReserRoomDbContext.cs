using HotelReservation.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.DbContexts
{
  public  class ReserRoomDbContext:DbContext
    {
        public ReserRoomDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<ReservationDTO> Reservations{ get; set; }
    }
}
