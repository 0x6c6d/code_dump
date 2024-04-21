namespace Persistence.Repositories;
public class StoragePlaceRepository : BaseRepository<StoragePlace>, IStoragePlaceRepository
{
    public StoragePlaceRepository(DataContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> IsStoragePlaceNameUnique(string name)
    {
        var match = _dbContext.StoragePlaces.Any(a => a.Name.ToLower() == name.ToLower());
        return Task.FromResult(match);
    }
}
