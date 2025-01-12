using FIAP.PhaseOne.Api.Mapping;
using Serilog;
using Serilog.Extensions.Logging;

namespace FIAP.PhaseOne.Api;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApiService(this IServiceCollection services)
    {

        var loggerConfig = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithProperty("ApplicationName", "FIAP.PhaseOne.Api")
            .WriteTo.Console()
            .CreateLogger();

        services.AddSingleton<ILoggerFactory>(new SerilogLoggerFactory(loggerConfig));
        services.AddLogging();

        services.AddAutoMapper(typeof(MappingProfile));
        
        return services;
    }
}
