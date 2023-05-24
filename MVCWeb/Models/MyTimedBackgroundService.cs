using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace MVCWeb.Models
{
    /// <summary>
    /// startDateTime : ex: From today @ 9gAM
    /// intervaltype: minute,hour,day,week: ex minute
    /// interval: number is length of interval: 1
    /// job task: Check and send batch email (of news letter)
    /// 
    /// Summary Desc: 
    /// each 1 minute (run task) send email of 
    /// news letter to customer
    /// </summary>
    public sealed class MyTimedBackgroundService : BackgroundService
    {
        /// <summary>
        /// Kiem tra xem co dung thoi diem de lam viec
        /// ex:
        /// vao ngay do tro di
        /// vao dung gio do
        /// chay task do
        /// 
        /// </summary>
        /// <param name="startDateTime"> ngay bat dau, va thoi diem chay</param>
        /// <param name="intervaltype">daily</param>
        /// <param name="intervallength">1</param>
        /// <returns></returns>
        private static bool TimeToDo(DateTime startDate,
            DateTime startTime,
            IntervalType intervaltype,
            int intervallength
            )
        {

            bool startNow = false;
            //daily at 8g45 everyday
            if (intervaltype== IntervalType.daily)
            {
                //neu thoi diem kiem tra >ngay qui dinh thi kiem tra tG
                if (DateTime.Now >= startDate) 
                {
                    string timeString = startTime.ToString("t");
                    string datestring = DateTime.Now.ToString("d");
                    string fulltime = datestring + " " + timeString;
                    DateTime todoTime= DateTime.Parse(fulltime);
                    //neu tg
                    startNow= DateTime.Now == todoTime;
                }
            }

            // tiep tuc cho cac interval khac
            if (intervaltype == IntervalType.weekly)
            {
                //... logic check
            }

                return startNow;
        }
        private static int SecondsUntilMidnight()
        {
            return (int)(DateTime.Today.AddDays(1.0) - DateTime.Now).TotalSeconds;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var countdown = SecondsUntilMidnight();

            while (!stoppingToken.IsCancellationRequested)
            {
                if (countdown-- <= 0)
                {
                    try
                    {
                        await OnTimerFiredAsync(stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        // TODO: log exception
                    }
                    finally
                    {
                        countdown = SecondsUntilMidnight();
                    }
                }
                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task OnTimerFiredAsync(CancellationToken stoppingToken)
        {
            // do your work here
            Debug.WriteLine("Simulating heavy I/O bound work");
            await Task.Delay(2000, stoppingToken);
        }
    }
    public enum IntervalType
    { 
        daily, //cach 1 ngay chay 1 lan
        weekly // 1 tuan chay 1 lan
    }

}
