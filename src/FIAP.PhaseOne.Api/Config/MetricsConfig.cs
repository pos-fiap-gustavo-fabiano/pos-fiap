﻿using System.Diagnostics.Metrics;
using System.Diagnostics;
using OpenTelemetry.Metrics;

namespace FIAP.PhaseOne.Api.Config
{
    public static class MetricsConfig
    {
        public static IServiceCollection AddMetricsConfig(this IServiceCollection services)
        {
            var meter = new Meter("Custom-Meter");
            var gauge_memory = meter.CreateObservableGauge<long>("memory_application", () => Process.GetCurrentProcess().WorkingSet64);
            var test_metric = meter.CreateCounter<long>("metric_counter");
            var gauge_cpu = meter.CreateObservableGauge<double>("cpu_usage", () => GetCpuUsage());
            services.AddOpenTelemetry()
            .WithMetrics(x =>
            {
                x.AddHttpClientInstrumentation();
                x.AddAspNetCoreInstrumentation();
                x.AddMeter("Microsoft.AspNetCore.Diagnostics");
                x.AddMeter("Custom-Meter");
                x.AddConsoleExporter();
                x.AddPrometheusExporter();
            });
            return services;
        }
        private static double GetCpuUsage()
        {
            var startTime = DateTime.UtcNow;
            var startCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
            Thread.Sleep(500); var endTime = DateTime.UtcNow;
            var endCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
            var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
            var totalMsPassed = (endTime - startTime).TotalMilliseconds;
            var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed) * 100; return cpuUsageTotal;
        }
    }
}
