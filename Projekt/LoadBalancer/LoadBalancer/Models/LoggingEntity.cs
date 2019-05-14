using System;
using System.Net.Http;

namespace LoadBalancer.Models
{
    public class LoggingEntity
    {
        public DateTime Time { get; set; }
        public string RequestUrl { get; set; }
        public string Method { get; set; }
        public Instance Instance { get; set; }
        public string RequestToInstanceUrl { get; set; }
        public int ResponseStatusCode { get; set; }

        public LoggingEntity(string requestUrl, string method, Instance instance, string requestToInstanceUrl, int responseStatusCode)
        {
            Time = DateTime.UtcNow;
            RequestUrl = requestUrl;
            Method = method;
            Instance = instance;
            RequestToInstanceUrl = requestToInstanceUrl;
            ResponseStatusCode = responseStatusCode;
        }

        public LoggingEntity(DateTime time, string requestUrl, string method, Instance instance, string requestToInstanceUrl, int responseStatusCode)
        {
            Time = time;
            RequestUrl = requestUrl;
            Method = method;
            Instance = instance;
            RequestToInstanceUrl = requestToInstanceUrl;
            ResponseStatusCode = responseStatusCode;
        }

        public override string ToString()
        {
            return $"{Time} {RequestUrl} {Method} {Instance} {RequestToInstanceUrl} {ResponseStatusCode}";
        }
    }
}