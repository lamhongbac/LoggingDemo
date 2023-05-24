using Microsoft.VisualBasic;
using System;

namespace MVCWeb.Models
{
    public class Schedule
    {
        public ERepeatType RepeatType { get; set; } //daily , hourly, weekly
        public DateAndTime  StartDate{ get; set; } //ngay bat dau
        public DateAndTime EndDate { get;set; } //ngay ket thuc
        public int StartTime{ get; set; } //tg bat dau
        public int EndTime { get; set; } //tg ket thuc

    }
    public enum ERepeatType
    {
        None=0,
        Hourly=1,
        Daily=2,
        Weekly=3,
        Monthly=4, Quaterly=5,HaftYearly, Yearly
    }
}
