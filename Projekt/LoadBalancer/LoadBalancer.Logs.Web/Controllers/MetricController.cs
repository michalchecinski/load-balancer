using System;
using AutoMapper;
using LoadBalancer.Logs.Web.Logic;
using Microsoft.AspNetCore.Mvc;

namespace LoadBalancer.Logs.Web.Controllers
{
    public class MetricController : Controller
    {
        private readonly IMapper _mapper;

        public MetricController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var logs = Logger.GetLogs();
            var now = DateTime.UtcNow.AddSeconds(-DateTime.UtcNow.Second);
            var metrics = MetricsService.CountMetrics(logs, now.AddHours(-1), now);

            return View(metrics);
        }

        public IActionResult Instance()
        {
            return GetLastInstanceHours(1);
        }

        [HttpGet]
        public IActionResult GetLastInstanceHours([FromQuery] int hours)
        {
            var logs = Logger.GetLogs();
            var now = DateTime.UtcNow.AddSeconds(-DateTime.UtcNow.Second);
            var instanceMetrics = MetricsService.CountInstanceMetrics(logs, now.AddHours(-hours), now, _mapper);

            return View("Instance", instanceMetrics);
        }

        [HttpGet]
        public IActionResult GetLastInstanceDays([FromQuery] int days)
        {
            var logs = Logger.GetLogs();
            var now = DateTime.UtcNow.AddSeconds(-DateTime.UtcNow.Second);
            var instanceMetrics = MetricsService.CountInstanceMetrics(logs, now.AddDays(-days), now, _mapper);

            return View("Instance", instanceMetrics);
        }

        [HttpGet]
        public IActionResult GetLastHours([FromQuery] int hours)
        {
            var logs = Logger.GetLogs();
            var now = DateTime.UtcNow.AddSeconds(-DateTime.UtcNow.Second);
            var metrics = MetricsService.CountMetrics(logs, now.AddHours(-hours), now);

            return View("Index", metrics);
        }

        [HttpGet]
        public IActionResult GetLastDays([FromQuery] int days)
        {
            var logs = Logger.GetLogs();
            var now = DateTime.UtcNow.AddSeconds(-DateTime.UtcNow.Second);
            var metrics = MetricsService.CountMetrics(logs, now.AddDays(-days), now);

            return View("Index",metrics);
        }
    }
}