using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.DTOs
{
 public   class ReservationDTO
    {
        [Key]
        public Guid Id { get; set; }
        public int Number { get ; set; }
        public int FloorNumber { get ; set ; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        //public string CustName { get => custName; set => custName = value; }
        //public RoomID Room { get => room; set => room = value; }
        public string UserName { get; set; }


    }
}
