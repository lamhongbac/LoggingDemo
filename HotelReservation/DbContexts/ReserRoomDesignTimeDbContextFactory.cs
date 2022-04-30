using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.DbContexts
{
    public class ReserRoomDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReserRoomDbContext>
    {
        public ReserRoomDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=ReserRoom.Db").Options;
            return new ReserRoomDbContext(options);
        }
    }
}
