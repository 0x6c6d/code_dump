using Backend.Features.Client.Models;
using Backend.Features.Command.Models;
using Backend.Features.Settings.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Common.Persistence;

public class DataContext : DbContext
{
    public DataContext() { }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        
        const string connectionString = "Data Source=/home/lucas/workspace/code/db/c2.sqlite";
        optionsBuilder.UseSqlite(connectionString);
    }
    
    public DbSet<ClientModel> Clients => Set<ClientModel>();
    public DbSet<CommandModel> Commands => Set<CommandModel>();
    public DbSet<SettingsModel> Settings => Set<SettingsModel>();
}