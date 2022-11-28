using Notification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notification.Interfaces
{
    public interface INotiService
    {
        List<Noti> GetNotifications(int TouserID, bool isUnread);
    }
}
