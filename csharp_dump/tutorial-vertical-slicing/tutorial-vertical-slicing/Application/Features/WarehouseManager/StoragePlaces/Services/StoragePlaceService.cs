using Application.Features.WarehouseManager.StoragePlaces.Models;
using Application.Features.WarehouseManager.StoragePlaces.Operations.Create;
using Application.Features.WarehouseManager.StoragePlaces.Operations.Delete;
using Application.Features.WarehouseManager.StoragePlaces.Operations.Read.All;
using Application.Features.WarehouseManager.StoragePlaces.Operations.Read.One;
using Application.Features.WarehouseManager.StoragePlaces.Operations.Update;

namespace Application.Features.WarehouseManager.StoragePlaces.Services;
public class StoragePlaceService : IStoragePlaceService
{
    private readonly IMediator _mediator;

    public StoragePlaceService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ServiceResponse<StoragePlace>> GetStoragePlaceAsync(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetStoragePlaceRequest { Id = id });
            if (result == null)
            {
                return new ServiceResponse<StoragePlace>
                {
                    Success = false,
                    Message = $"Keinen Lagerplatz mit der ID {id} gefunden."
                };
            }

            var storagePlace = StoragePlaceMapper.GetStoragePlaceFromGetStoragePlaceReturn(result);
            return new ServiceResponse<StoragePlace> { Data = storagePlace };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<StoragePlace>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<List<StoragePlace>>> GetStoragePlacesAsync()
    {
        try
        {
            var result = await _mediator.Send(new GetStoragePlacesRequest());
            if (result == null)
            {
                return new ServiceResponse<List<StoragePlace>>
                {
                    Success = false,
                    Message = "Keinen Lagerplatz gefunden."
                };
            }

            var storagePlaces = StoragePlaceMapper.GetStoragePlacesFromGetStoragePlacesReturn(result);
            return new ServiceResponse<List<StoragePlace>> { Data = storagePlaces };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<StoragePlace>>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<Guid>> CreateStoragePlaceAsync(string name)
    {
        try
        {
            var result = await _mediator.Send(new CreateStoragePlaceRequest() { Name = name });
            if (result == Guid.Empty)
            {
                return new ServiceResponse<Guid>
                {
                    Success = false,
                    Message = $"Fehler beim Erstellen des Lagerplatzes."
                };
            }

            return new ServiceResponse<Guid> { Data = result };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<Guid>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<bool>> UpdateStoragePlaceAsync(Guid id, string oldName, string newName)
    {
        try
        {
            if (oldName.Equals("Im Einsatz", StringComparison.CurrentCultureIgnoreCase))
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"'{oldName}' kann nicht geupdated werden."
                };
            }

            await _mediator.Send(new UpdateStoragePlaceRequest() { Id = id, Name = newName });
            return new ServiceResponse<bool> { Data = true };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<bool>> DeleteStoragePlaceAsync(Guid id, string oldName)
    {
        try
        {
            if (oldName.Equals("Im Einsatz", StringComparison.CurrentCultureIgnoreCase))
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"'{oldName}' kann nicht gelöscht werden."
                };
            }

            await _mediator.Send(new DeleteStoragePlaceRequest() { Id = id });
            return new ServiceResponse<bool> { Data = true };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
}
