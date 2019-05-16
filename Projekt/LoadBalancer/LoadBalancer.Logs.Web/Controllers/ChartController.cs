using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoadBalancer.Logs.Web.Logic;
using LoadBalancer.Logs.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoadBalancer.Logs.Web.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult Index()
        {
            return GetLastMinutes(60);
        }

        [HttpGet]
        private IActionResult GetLastMinutes(int minutes)
        {
            if (minutes > 60)
            {
                throw new ArgumentException();
            }

            var logs = Logger.GetLogs();
            var now = DateTime.UtcNow.AddSeconds(-DateTime.UtcNow.Second);

            var model = new ChartViewModel();
            model.FromTime = now.AddMinutes(-minutes);
            model.ToTime = now;
            model.ChartDataModels = new List<Metrics>();
            model.ChartType = ChartType.Time;
            int sub = minutes >= 30 ? 5 : 1; 
            for (int i = minutes; i >= 0; i-=sub)
            {
                var metrics = MetricsService.CountMetrics(logs, now.AddMinutes(-i), now.AddMinutes(-i).AddSeconds(-now.Second).AddSeconds(59));
                model.ChartDataModels.Add(metrics);
            }

            return View("Index", model);
        }

        [HttpGet]
        public IActionResult GetLastHours([FromQuery] int hours)
        {
            if (hours == 1)
            {
                return GetLastMinutes(60);
            }
            if (hours > 24)
            {
                throw new ArgumentException();
            }

            var logs = Logger.GetLogs();
            var now = DateTime.UtcNow.AddSeconds(-DateTime.UtcNow.Second);
            now = now.AddMinutes(-now.Minute);

            var model = new ChartViewModel();
            model.FromTime = now.AddHours(-hours);
            model.ToTime = now.AddHours(1);
            model.ChartDataModels = new List<Metrics>();
            model.ChartType = ChartType.Hour;

            for (int i = hours; i >= 0; i--)
            {
                var metrics = MetricsService.CountMetrics(logs, now.AddHours(-i), now.AddHours(-i).AddMinutes(-now.Minute).AddMinutes(59).AddSeconds(-now.Second).AddSeconds(59));
                model.ChartDataModels.Add(metrics);
            }            

            return View("Index", model);
        }

        [HttpGet]
        public IActionResult GetLastDays([FromQuery] int days)
        {
            if (days == 1)
            {
                return GetLastHours(24);
            }
            var logs = Logger.GetLogs();
            var now = DateTime.UtcNow.AddSeconds(-DateTime.UtcNow.Second);

            var model = new ChartViewModel();
            model.FromTime = now.AddDays(-days);
            model.ToTime = now;
            model.ChartDataModels = new List<Metrics>();

            for (int i = days; i >= 0; i--)
            {
                var metrics = MetricsService.CountMetrics(logs, now.AddDays(-i), now.AddDays(-i).AddHours(-now.Hour).AddHours(23).AddMinutes(-now.Minute).AddMinutes(59).AddSeconds(-now.Second).AddSeconds(59));
                model.ChartDataModels.Add(metrics);
            }

            return View("Index", model);
        }
    }
}