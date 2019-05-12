using LoadBalancer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoadBalancer.Middleware
{
    public class ForwardingMiddleware
    {
        private readonly HttpClient _httpClient;

        public ForwardingMiddleware(RequestDelegate next)
        {
            _httpClient = new HttpClient(new HttpClientHandler());
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.Headers["X-Forwarded-For"] = context.Connection.RemoteIpAddress.ToString();
            context.Request.Headers["X-Forwarded-Proto"] = context.Request.Protocol.ToString();
            int port = context.Request.Host.Port ?? (context.Request.IsHttps ? 443 : 80);
            context.Request.Headers["X-Forwarded-Port"] = port.ToString();

            var instance = context.Items["destination"] as Instance;

            await HandleHttpRequest(context, instance, instance.Ip, instance.Port, "http");
        }

        private async Task HandleHttpRequest(HttpContext context, Instance destination, string host, int port, string scheme)
        {

            var requestMessage = new HttpRequestMessage();
            var requestMethod = context.Request.Method;

            if (!HttpMethods.IsGet(requestMethod) && !HttpMethods.IsHead(requestMethod) && !HttpMethods.IsDelete(requestMethod) && !HttpMethods.IsTrace(requestMethod))
            {
                var streamContent = new StreamContent(context.Request.Body);
                requestMessage.Content = streamContent;
            }

            // All request headers and cookies must be transferend to remote server. Some headers will be skipped
            foreach (var header in context.Request.Headers)
            {
                if (!requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()) && requestMessage.Content != null)
                {
                    requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                }
            }

            requestMessage.Headers.Host = host;
            //recreate remote url
            string uriString = GetUri(context, host, port, scheme);
            requestMessage.RequestUri = new Uri(uriString);
            requestMessage.Method = new HttpMethod(context.Request.Method);
            using (var responseMessage = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted))
            {
                context.Response.StatusCode = (int)responseMessage.StatusCode;
                foreach (var header in responseMessage.Headers)
                {
                    context.Response.Headers[header.Key] = header.Value.ToArray();
                }

                foreach (var header in responseMessage.Content.Headers)
                {
                    context.Response.Headers[header.Key] = header.Value.ToArray();
                }

                var buffer = new byte[Int32.Parse(context.Response.Headers["Content-Length"])];

                using (var responseStream = await responseMessage.Content.ReadAsStreamAsync())
                {

                    int len = 0;
                    int full = 0;
                    while ((len = await responseStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await context.Response.Body.WriteAsync(buffer, 0, buffer.Length);
                        full += buffer.Length;
                    }

                    //context.Response.Headers.Remove("transfer-encoding");
                }
            }
        }

        private static string GetUri(HttpContext context, string host, int? port, string scheme)
        {
            var urlPort = "";
            if (port.HasValue
                && !(port.Value == 443 && "https".Equals(scheme, StringComparison.InvariantCultureIgnoreCase))
                && !(port.Value == 80 && "http".Equals(scheme, StringComparison.InvariantCultureIgnoreCase))
                )
            {
                urlPort = ":" + port.Value;
            }
            return $"{scheme}://{host}{urlPort}{context.Request.PathBase}{context.Request.Path}{context.Request.QueryString}";
        }
    }
}
