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
        private static List<LoggingEntity> loggingEntities = new List<LoggingEntity>();

        public static void Log(LoggingEntity loggingEntity)
        {
            loggingEntities.Add(loggingEntity);
        }

        public static void Log(string requestUrl, string method, Instance instance, string requestToInstanceUrl, int responseStatusCode)
        {
            Log(new LoggingEntity(requestUrl, method, instance, requestToInstanceUrl, responseStatusCode));
        }
    }
}
