using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Domain.Roles;
using FishbowlSoftware.Planner.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FishbowlSoftware.Planner.DbMigrator.Data;

public partial class SeedData : BackgroundService
{
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly ILogger<SeedData> _logger;
    private readonly IConfiguration _configuration;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IUnitOfWork _uow;
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public SeedData(
        IHostApplicationLifetime appLifetime,
        IConfiguration configuration,
        ILogger<SeedData> logger,
        IServiceScopeFactory serviceScopeFactory,
        IUnitOfWork uow,
        RoleManager<Role> roleManager,
        UserManager<User> userManager)
    {
        _appLifetime = appLifetime;
        _configuration = configuration;
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _uow = uow;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        await SeedDatabaseAsync(environment ?? "Development");
        _appLifetime.StopApplication();
    }

    private async Task SeedDatabaseAsync(string environment = "Development")
    {
        var scope = _serviceScopeFactory.CreateScope();
        var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        _logger.LogInformation("Initializing the database...");
        await MigrateDatabaseAsync(applicationDbContext);
        _logger.LogInformation("Successfully initialized the database");

        switch (environment)
        {
            case "Development":
                await SeedDatabaseAsync_Development();
                break;
            case "Production":
                await SeedDatabaseAsync_Production();
                break;
        }
    }

    private static async Task MigrateDatabaseAsync(DbContext databaseContext)
    {
        await databaseContext.Database.MigrateAsync();
    }

    private async Task AddBuiltInRolesAsync()
    {
        foreach (var roleName in BuiltInRoles.GetRoleNames())
        {
            var role = new Role { Name = roleName };

            var existingRole = await _roleManager.FindByNameAsync(role.Name);
            if (existingRole is not null)
            {
                continue;
            }

            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                _logger.LogInformation("Added the '{RoleName}' role", role.Name);
            }
            else
            {
                _logger.LogError("Failed to add the '{RoleName}' role", role.Name);
            }
        }
    }

    private async Task AddAdminAsync()
    {
        var adminData = _configuration.GetRequiredSection("Admin").Get<UserData>();

        if (adminData is null)
        {
            throw new InvalidOperationException("Admin user data is null, specify admin user data in the appsettings.json file");
        }

        var adminUser = await _userManager.FindByEmailAsync(adminData.Email);

        if (adminUser is null)
        {
            adminUser = new User
            {
                UserName = adminData.Email,
                Email = adminData.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(adminUser, adminData.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.First().Description);
            }

            _logger.LogInformation("Created an admin user '{Admin}'", adminUser.UserName);
        }

        var hasAdminRole = await _userManager.IsInRoleAsync(adminUser, BuiltInRoles.Admin);

        if (!hasAdminRole)
        {
            await _userManager.AddToRoleAsync(adminUser, BuiltInRoles.Admin);
            _logger.LogInformation("Added 'admin' role to the user '{Admin}'", adminUser.UserName);
        }
    }
}

internal record UserData(string Email, string Password);
