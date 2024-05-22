using Application.Features.StoreManager.Events.Models;
using Application.Features.StoreManager.Stores.Models;
using Application.Features.StoreManager.Technologies.Models;
using Application.Features.WarehouseManager.Articles.Models;
using Application.Features.WarehouseManager.Categories.Models;
using Application.Features.WarehouseManager.Groups.Models;
using Application.Features.WarehouseManager.OperationAreas.Models;
using Application.Features.WarehouseManager.Procurements.Models;
using Application.Features.WarehouseManager.StoragePlaces.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Persistence;
public class DataContext : DbContext
{
    public DataContext() { }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=DatabaseName;Trusted_Connection=True;TrustServerCertificate=true;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    // Store Kennung
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<EventType> EventTypes => Set<EventType>();
    public DbSet<Technology> Technologies => Set<Technology>();

    // Warehouse Manager
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Group> Groups => Set<Group>();
    public DbSet<OperationArea> OperationAreas => Set<OperationArea>();
    public DbSet<Procurement> Procurements => Set<Procurement>();
    public DbSet<StoragePlace> StoragePlaces => Set<StoragePlace>();
}
