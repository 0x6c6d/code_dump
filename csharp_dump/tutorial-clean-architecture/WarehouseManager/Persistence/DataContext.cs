namespace Persistence;
public class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=WarehouseIT;Trusted_Connection=True;TrustServerCertificate=true;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Group> Groups => Set<Group>();
    public DbSet<OperationArea> OperationAreas => Set<OperationArea>();
    public DbSet<Procurement> Procurements => Set<Procurement>();
    public DbSet<StoragePlace> StoragePlaces => Set<StoragePlace>();
}