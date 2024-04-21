
namespace Web.Services.DA;

public interface IDataAccessEntityService<TEntity>
{
    Task<DataAccessResponse<Guid>> CreateEntityAsync(string apiRoute, string name);
    Task<DataAccessResponse<bool>> DeleteEntityAsync(string apiRoute, Guid id);
    Task<DataAccessResponse<List<TEntity>>> GetEntitiesAsync(string apiRoute);
    Task<DataAccessResponse<TEntity>> GetEntityAsync(string apiRoute, Guid id);
    Task<DataAccessResponse<bool>> UpdateEntityAsync(string apiRoute, Guid id, string name);
}