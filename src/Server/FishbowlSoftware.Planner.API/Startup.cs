using FishbowlSoftware.Planner.API.StartupConfigurations;
using FishbowlSoftware.Planner.API.Middlewares;
using FishbowlSoftware.Planner.Application;
using FishbowlSoftware.Planner.Domain;
using FishbowlSoftware.Planner.Infrastructure;
using Serilog;

namespace FishbowlSoftware.Planner.API;

public static class Startup
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDomainLayer();
        builder.Services.AddApplicationLayer();
        builder.Services.AddInfrastructureLayer(builder.Configuration);

        builder.ConfigureControllers();
        builder.ConfigureAuthentication();
        builder.ConfigureCors();
        builder.ConfigureLogger();
        builder.ConfigureSwagger();
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(SwaggerConfiguration.GetSwaggerUIOptions(app.Configuration));
        }

        app.UseHttpsRedirection();
        app.UseCors(app.Environment.IsDevelopment() ? "AnyCors" : "DefaultCors");

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCustomExceptionHandler();
        app.MapControllers();
        return app;
    }
}
