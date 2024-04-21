namespace Web.Services.BL;

public class EntityService<TEntity> : IEntityService<TEntity>
{
    private readonly IDataAccessEntityService<TEntity> _dataAccessService;

    public EntityService(IDataAccessEntityService<TEntity> dataAccessService)
    {
        _dataAccessService = dataAccessService;
    }

    public async Task<DataAccessResponse<TEntity>> GetEntity(string apiRoute, Guid id)
    {
        if (id == Guid.Empty)
            return new DataAccessResponse<TEntity> 
            {
                Success = false,
                Message = $"{nameof(TEntity)}Id is empty."
            };

        var result = await _dataAccessService.GetEntityAsync(apiRoute, id);
        return result;
    }

    public async Task<DataAccessResponse<List<TEntity>>> GetEntities(string apiRoute)
    {
        var result = await _dataAccessService.GetEntitiesAsync(apiRoute);
        return result;
    }

    public async Task<DataAccessResponse<Guid>> CreateEntity(string apiRoute, string name)
    {
        var result = await _dataAccessService.CreateEntityAsync(apiRoute, name);
        return result;
    }

    public async Task<DataAccessResponse<bool>> UpdateEntity(string apiRoute, Guid id, string name)
    {
        var result = await _dataAccessService.UpdateEntityAsync(apiRoute, id, name);
        return result;
    }

    public async Task<DataAccessResponse<bool>> DeleteEntity(string apiRoute, Guid id)
    {
        var result = await _dataAccessService.DeleteEntityAsync(apiRoute, id);
        return result;
    }
}
