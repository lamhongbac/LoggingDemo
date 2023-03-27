using System;
using System.ComponentModel.DataAnnotations;

namespace MVCWeb.Models
{
    /// <summary>
    /// Moi cua hang se co 1 Mac Address
    /// Mac addess tuong duong ma cua hang
    /// DS nhan vien se dc gan ma cua hang (UserID--MACAddress)
    /// viec check in voi rule la: chi co nV cua CH moi dc phep
    /// 
    /// </summary>
    public class TimeAttendanceModel
    {
        [Display(Name = "UserID")]
        public string UserID { get; set; }

        [Display(Name ="Network Mac Address")]
        public string MacAddress { get; set; }

        public string AttendanceAction { get; set; } //
        public string Message { get; set; }
        public DateTime  AttendanceTime { get; set; }

    }
}
