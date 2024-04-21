namespace Application.Contracts.Persistence;
public interface IStoragePlaceRepository : IRepository<StoragePlace>
{
    Task<bool> IsStoragePlaceNameUnique(string name);
}
