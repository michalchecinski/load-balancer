using LoadBalancer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer
{
    public class Logger
    {
        private static List<LoggingEntity> logs = new List<LoggingEntity>();

        public static List<LoggingEntity> GetLogs()
        {
            return logs;
        }

        public static void Log(LoggingEntity loggingEntity)
        {
            logs.Add(loggingEntity);
        }

        public static void Log(string requestUrl, string method, Instance instance, string requestToInstanceUrl, int responseStatusCode)
        {
            Log(new LoggingEntity(requestUrl, method, instance, requestToInstanceUrl, responseStatusCode));
        }
    }
}
