using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.DbContexts
{
   public class ReserRoomDbContextFactory
    {
        private readonly string _connectionString;

      public  ReserRoomDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public ReserRoomDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;
            return new ReserRoomDbContext(options);
        }
    }
}
