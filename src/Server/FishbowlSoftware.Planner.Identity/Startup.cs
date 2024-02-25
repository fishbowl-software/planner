using FishbowlSoftware.Planner.Domain;
using FishbowlSoftware.Planner.Identity.StartupConfigurations;
using FishbowlSoftware.Planner.Infrastructure;
using Serilog;

namespace FishbowlSoftware.Planner.Identity;

public static class Startup
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDomainLayer();
        builder.Services.AddInfrastructureLayer(builder.Configuration);
        builder.Services.AddRazorPages();

        builder.ConfigureAuthentication();
        builder.ConfigureCors();
        builder.ConfigureLogger();
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        
        app.UseHttpsRedirection();
        app.UseCors(app.Environment.IsDevelopment() ? "AnyCors" : "DefaultCors");
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapRazorPages();
        return app;
    }
}
