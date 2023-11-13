using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowService
{
    /// <summary>
    /// nguyen ly la cu 1second service check 1 lan
    /// thi du khi service first start =>thoi diem bat dau la 3g30 (interval=1, hourly), thoi diem next la 4g30
    /// thoi diem kiem tra la 3g40>3g30, => kg chay =>update  Start=nextStart=4g30 va nextStart= 5g30
    /// khi thoi diem kiem tra =4g30=> chay , sau do update start=NextStart=> nextStart=Start+1=6g30
    /// </summary>
    public class Scheduler
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StartAt { get; set; } //dieu kien de bat dau > = StartAt
        public EIntervalType InterValType { get; set; }
        public int NextStart { get; set;} //la thoi diem de kiem tra moi khi thoa dk chay

    }
    public enum EIntervalType
    {
        Minute,Hourly,Daily,Weekly,Monthly,Yearly   
    }

  
}
