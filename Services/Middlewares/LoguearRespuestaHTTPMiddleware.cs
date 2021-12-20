using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Middlewares
{
    public static class LoguearRespuestaHTTPMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoguearRespuestaHTTP(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoguearRespuestaHTTPMiddleware>();
        }
    }


    public class LoguearRespuestaHTTPMiddleware
    {
        
        private readonly RequestDelegate next;
        private readonly ILogger<LoguearRespuestaHTTPMiddleware> logger;

        public LoguearRespuestaHTTPMiddleware(RequestDelegate next,
            ILogger<LoguearRespuestaHTTPMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        //Invoke o InvokeAsync
        public async Task InvokeAsync(HttpContext context)
        {
            using (var ms = new MemoryStream())
            {
                var originalBodyResponse = context.Response.Body;
                context.Response.Body = ms;

                await next(context);

                ms.Seek(0, SeekOrigin.Begin);
                string response = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);

                await ms.CopyToAsync(originalBodyResponse);
                context.Response.Body = originalBodyResponse;

                logger.LogInformation(response);
            }
        }

    }
}
