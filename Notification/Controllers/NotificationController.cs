using Microsoft.AspNetCore.Mvc;
using Notification.Interfaces;
using Notification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notification.Controllers
{
    public class NotificationController : Controller
    {
        public NotificationController(INotiService notificationService)
        {
            _notificationService = notificationService;
        }
           //
           List<Noti> notifications;
        INotiService _notificationService;

        public IActionResult AllNotifications ()
        {
            int userID = 2;
            notifications = _notificationService.GetNotifications(userID, false);
            return View();
        }
        public IActionResult OneSignalNotification()
        {
            return View();

        }


        public JsonResult GetNotifications(bool getUnread = false)
        {
            int userID = 2;
            notifications = _notificationService.GetNotifications(userID, getUnread);
            return Json(notifications);
        }
    }
}
