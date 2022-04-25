using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models
{
   public class RoomID
    {
       public RoomID(int roomNumber, int floorNumber)
        {
            Number = roomNumber;
            FloorNumber = floorNumber;
        }
        public int Number { get => number; set => number = value; }
        public int FloorNumber { get => floorNumber; set => floorNumber = value; }

        int number;//so phong
        
        int floorNumber;
        public override string ToString()
        {
            return Number.ToString();
        }
        /// <summary>
        /// Kiem tra 1 phong co = voi phong nay hay khong
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is RoomID room && room.Number == Number && room.FloorNumber == FloorNumber;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Number,FloorNumber);
        }
        public static bool  operator == (RoomID room1, RoomID room2)
        {
            if (room1==null && room2==null)
            {
                return true;
            }

         return room1!=null && room1.Number == room2.Number && room1.FloorNumber == room2.FloorNumber;
        }
        public static bool operator !=(RoomID room1, RoomID room2)
        {
            return !(room1 == room2);//&& room.Number == Number && room.FloorNumber == FloorNumber;
        }
    }
}
