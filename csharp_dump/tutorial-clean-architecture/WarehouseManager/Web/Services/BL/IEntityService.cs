
namespace Web.Services.BL;

public interface IEntityService<TEntity>
{
    Task<DataAccessResponse<Guid>> CreateEntity(string apiRoute, string name);
    Task<DataAccessResponse<bool>> DeleteEntity(string apiRoute, Guid id);
    Task<DataAccessResponse<List<TEntity>>> GetEntities(string apiRoute);
    Task<DataAccessResponse<TEntity>> GetEntity(string apiRoute, Guid id);
    Task<DataAccessResponse<bool>> UpdateEntity(string apiRoute, Guid id, string name);
}