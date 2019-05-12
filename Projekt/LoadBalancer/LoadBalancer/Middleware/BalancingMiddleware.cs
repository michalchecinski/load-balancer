using LoadBalancer.BalancingAlgorithms;
using LoadBalancer.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LoadBalancer.Middleware
{
    public class BalancingMiddleware
    {
        private readonly RequestDelegate _next;

        public BalancingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var balancerAlgorithmSetting = LoadBalancerSettings.Current.BalancingAlgorithm.ToLowerInvariant().Replace(" ", "");

            BalancingAlgorithm balancerAlgorithm = null;

            if (balancerAlgorithmSetting == "roundrobin")
            {
                balancerAlgorithm = new RoundRobinAlgorithm();
            }
            else if (balancerAlgorithmSetting == "random")
            {
                balancerAlgorithm = new RandomAlgorithm();
            }
            else if (balancerAlgorithmSetting == "connectioncount")
            {
                balancerAlgorithm = new ConnectionCountAlgorithm();
            }

            context.Items["destination"] = balancerAlgorithm.GetInstance();

            await _next.Invoke(context);
        }
    }
}
