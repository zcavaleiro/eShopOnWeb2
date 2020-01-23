using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Web.Middleware
{
    public class MeasureRequestExecutionTime
    {
        private readonly ILogger<MeasureRequestExecutionTime> _logger;
        private readonly RequestDelegate _next;
        public MeasureRequestExecutionTime(
            RequestDelegate next,
            ILogger<MeasureRequestExecutionTime> logger
        ) {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext) {
            // Pedido
            await _next(httpContext);
            // Resposta
        }
    }
}