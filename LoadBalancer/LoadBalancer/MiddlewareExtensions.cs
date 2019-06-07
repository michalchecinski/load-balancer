using LoadBalancer.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseBalancer(
           this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BalancingMiddleware>();
        }

        public static IApplicationBuilder UseForwarding(
           this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ForwardingMiddleware>();
        }
    }
}
