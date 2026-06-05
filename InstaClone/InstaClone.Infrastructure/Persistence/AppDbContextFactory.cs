using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InstaClone.Infrastructure.Persistence;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        string cwd = Directory.GetCurrentDirectory();
        var candidates = new[]
        {
            cwd,
            Path.GetFullPath(Path.Combine(cwd, "..", "InstaClone.Api")),
            Path.GetFullPath(Path.Combine(cwd, "..", "..", "InstaClone.Api")),
            AppContext.BaseDirectory
        };

        string basePath = candidates.FirstOrDefault(p => File.Exists(Path.Combine(p, "appsettings.json")));
        if (basePath == null)
        {
            throw new InvalidOperationException(
                "Could not find appsettings.json for design-time DbContext creation. Searched: " +
                string.Join(", ", candidates));
        }

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("DefaultConnection not found in configuration.");

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseSqlServer(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}