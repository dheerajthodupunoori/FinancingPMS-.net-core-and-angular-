using FinancingPMS.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FinancingPMS.Middlewares
{
    public class HttpRequestTime
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<HttpRequestTime> _logger;
        public HttpRequestTime(RequestDelegate next , ILogger<HttpRequestTime> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            Guid transactionID = Guid.NewGuid();
            context.Request.Headers.Add("transactionID", transactionID.ToString());

            FinancingPMSLogger logMessage = new FinancingPMSLogger
            {
                message = $"Started executing the Http Request at {time.Elapsed}",
                transactionID = transactionID.ToString()
            };

            _logger.LogInformation(message: logMessage.ToString());
            await _next.Invoke(context);
            time.Stop();

            logMessage.message = $"Completed executing the Http Request at {time.Elapsed}";
        }
    }

    public static class HttpRequestTimeMiddleware
    {
        public static IApplicationBuilder UseHttpRequestTimeMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HttpRequestTime>();
        }
    }
}
