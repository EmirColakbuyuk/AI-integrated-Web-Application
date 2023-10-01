using Microsoft.EntityFrameworkCore;
using CizgiWebServer.Model;

namespace CizgiWebServer.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Users> Users { get; set; }
    public DbSet<Tweets> Tweets { get; set; }
    public DbSet<Logs> Logs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration =
            new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json")
                .Build();

        var connectionString = configuration.GetConnectionString("MyConnectionString");
        optionsBuilder.UseSqlServer(connectionString);
    }
    
}