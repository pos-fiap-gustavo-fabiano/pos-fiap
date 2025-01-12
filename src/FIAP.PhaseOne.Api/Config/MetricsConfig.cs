using OpenTelemetry.Metrics;

namespace FIAP.PhaseOne.Api.Config
{
    public static class MetricsConfig
    {
        public static IServiceCollection AddMetricsConfig(this IServiceCollection services)
        {
            services.AddOpenTelemetry()
            .WithMetrics(x =>
            {
                x.AddPrometheusExporter();
                x.AddHttpClientInstrumentation(); 
                x.AddAspNetCoreInstrumentation();
            });
            return services;
        }

    }
}
