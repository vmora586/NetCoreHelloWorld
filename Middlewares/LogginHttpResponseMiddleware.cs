
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BooksApi.Middlewares
{
    public static class LogginHttpResponseMiddlewareExtension
    {
        public static IApplicationBuilder UseLogginHttpResponse(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogginHttpResponseMiddleware>();
        }
    }
    public class LogginHttpResponseMiddleware
    {
        private readonly RequestDelegate NextRequest;
        private readonly ILogger<LogginHttpResponseMiddleware> logger;
        public LogginHttpResponseMiddleware(RequestDelegate nextRequest,

                ILogger<LogginHttpResponseMiddleware> logger)
        {
            this.logger = logger;
            this.NextRequest = nextRequest;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var msg = new MemoryStream())
            {
                var originalResponseBody = context.Response.Body;
                context.Response.Body = msg;
                await NextRequest.Invoke(context);

                msg.Seek(0, SeekOrigin.Begin);
                string answer = new StreamReader(msg).ReadToEnd();
                msg.Seek(0, SeekOrigin.Begin);

                await msg.CopyToAsync(originalResponseBody);
                context.Response.Body = originalResponseBody;

                logger.LogInformation(answer);
            }
        }
    }
}