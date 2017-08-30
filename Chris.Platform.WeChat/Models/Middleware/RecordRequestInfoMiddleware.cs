using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Chris.Platform.WeChat.Models.Middleware
{
    public class RecordRequestInfoMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RecordRequestInfoMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RecordRequestInfoMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation($"User IP:{context.Connection.RemoteIpAddress}");
            _logger.LogInformation($"User RequestPath：{context.Request.Path}");
            await _next.Invoke(context);
        }
    }

    public static class RecordRequestInfoExtensions
    {
        /// <summary>
        /// 记录请求信息
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRecordRequestInfo(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RecordRequestInfoMiddleware>();
        }
    }
}