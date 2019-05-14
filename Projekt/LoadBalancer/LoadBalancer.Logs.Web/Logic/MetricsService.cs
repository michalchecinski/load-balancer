using AutoMapper;
using LoadBalancer.Logs.Web.Models;
using LoadBalancer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.Logs.Web.Logic
{
    public static class MetricsService
    {
        public static Metrics CountMetrics(List<LoggingEntity> logs, DateTime fromDateTime, DateTime toDateTime)
        {
            var timeLogs = logs.Where(x => x.Time >= fromDateTime && x.Time <= toDateTime);

            var metrics = new Metrics();
            metrics.RequestCount = timeLogs.Count();
            metrics.SuccessCount = timeLogs.Count(x => x.ResponseStatusCode >= 200 && x.ResponseStatusCode <= 299);
            metrics.SuccessPercent = metrics.RequestCount == 0 ? 0 : metrics.SuccessCount / metrics.RequestCount * 100;
            metrics.NotFoundCount = timeLogs.Count(x => x.ResponseStatusCode == 404);
            metrics.NotFoundPercent = metrics.RequestCount == 0 ? 0 : metrics.NotFoundCount / metrics.RequestCount * 100;
            metrics.FiveHundredCount = timeLogs.Count(x => x.ResponseStatusCode >= 500 && x.ResponseStatusCode <= 599);
            metrics.FiveHundredPercent = metrics.RequestCount == 0 ? 0 : metrics.FiveHundredCount / metrics.RequestCount * 100;
            metrics.FromDateTime = fromDateTime;
            metrics.ToDateTime = toDateTime;
            return metrics;
        }

        public static IEnumerable<InstanceMetrics> CountInstanceMetrics(List<LoggingEntity> logs, 
                                                                        DateTime fromDateTime, 
                                                                        DateTime toDateTime, 
                                                                        IMapper mapper)
        {
            var instances = logs.Select(x => x.Instance)
                                .Distinct(new InstanceComparer())
                                .ToList();

            var instanceMetricsList = new List<InstanceMetrics>();

            foreach (var instance in instances)
            {
                var timeLogs = logs.Where(x => x.Time >= fromDateTime && x.Time <= toDateTime);
                var instanceLogs = timeLogs.Where(x => x.Instance.Equals(instance))
                                           .ToList();

                InstanceMetrics metrics = mapper.Map<InstanceMetrics>(CountMetrics(instanceLogs, fromDateTime, toDateTime));
                metrics.Instance = instance;
                metrics.RequestPercentage = (double) metrics.RequestCount / timeLogs.Count() * 100;

                yield return metrics;
            }
        }
    }

    class InstanceComparer : IEqualityComparer<Instance>
    {
        public bool Equals(Instance i1, Instance i2)
        {
            return i1.Port == i2.Port && i1.Ip == i2.Ip;
        }

        public int GetHashCode(Instance obj)
        {
            return obj.Ip.GetHashCode() + obj.Port.GetHashCode();
        }
    }
}
