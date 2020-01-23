using Microsoft.AspNetCore.Builder;
using Web.Middleware;

namespace Web.Extensions.Middleware
{
    public static class BenchmarkingMiddlewareExtensions
    {
        public static void UseBenchmarking(this IApplicationBuilder app) {
            app.UseMiddleware<MeasureRequestExecutionTime>();
            // app.UseMiddleware<MeasureRequestExecutionTime>();
            // app.UseMiddleware<MeasureRequestExecutionTime>();
            // app.UseMiddleware<MeasureRequestExecutionTime>();
            // app.UseMiddleware<MeasureRequestExecutionTime>();
            // app.UseMiddleware<MeasureRequestExecutionTime>();
            
        }
    }
}