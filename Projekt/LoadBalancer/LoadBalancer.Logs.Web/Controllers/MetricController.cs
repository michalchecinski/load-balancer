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
            var logs = Logger.GetLogs();
            var now = DateTime.UtcNow.AddSeconds(-DateTime.UtcNow.Second);
            var instanceMetrics = MetricsService.CountInstanceMetrics(logs, now.AddHours(-1), now, _mapper);

            return View(instanceMetrics);
        }
    }
}