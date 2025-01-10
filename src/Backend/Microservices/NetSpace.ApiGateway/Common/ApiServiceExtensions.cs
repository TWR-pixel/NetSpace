using Serilog;

namespace NetSpace.ApiGateway.Common;

public static class ApiServiceExtensions
{
    public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();

        var logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .CreateLogger();

        builder.Logging.AddSerilog();

        builder.Host.UseSerilog((context, loggerConfig) =>
        {
            loggerConfig.ReadFrom.Configuration(context.Configuration);
        });

        return builder;
    }
}
