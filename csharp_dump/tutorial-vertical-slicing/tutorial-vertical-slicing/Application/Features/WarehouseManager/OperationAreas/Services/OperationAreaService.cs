using Application.Features.WarehouseManager.OperationAreas.Models;
using Application.Features.WarehouseManager.OperationAreas.Operations.Create;
using Application.Features.WarehouseManager.OperationAreas.Operations.Delete;
using Application.Features.WarehouseManager.OperationAreas.Operations.Read.All;
using Application.Features.WarehouseManager.OperationAreas.Operations.Read.One;
using Application.Features.WarehouseManager.OperationAreas.Operations.Update;

namespace Application.Features.WarehouseManager.OperationAreas.Services;
public class OperationAreaService : IOperationAreaService
{
    private readonly IMediator _mediator;

    public OperationAreaService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ServiceResponse<OperationArea>> GetOperationAreaAsync(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetOperationAreaRequest { Id = id });
            if (result == null)
            {
                return new ServiceResponse<OperationArea>
                {
                    Success = false,
                    Message = $"Kein Einsatzgebiet mit der ID {id} gefunden."
                };
            }

            var operationArea = OperationAreaMapper.GetOperationAreaFromGetOperationAreaReturn(result);
            return new ServiceResponse<OperationArea> { Data = operationArea };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<OperationArea>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<List<OperationArea>>> GetOperationAreasAsync()
    {
        try
        {
            var result = await _mediator.Send(new GetOperationAreasRequest());
            if (result == null)
            {
                return new ServiceResponse<List<OperationArea>>
                {
                    Success = false,
                    Message = "Keine Einsatzgebiete gefunden."
                };
            }

            var operationAreas = OperationAreaMapper.GetOperationAreasFromGetOperationAreasReturn(result);
            return new ServiceResponse<List<OperationArea>> { Data = operationAreas };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<OperationArea>>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<Guid>> CreateOperationAreaAsync(string name)
    {
        try
        {
            var result = await _mediator.Send(new CreateOperationAreaRequest() { Name = name });
            if (result == Guid.Empty)
            {
                return new ServiceResponse<Guid>
                {
                    Success = false,
                    Message = $"Fehler beim Erstellen des Einsatzgebietes."
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

    public async Task<ServiceResponse<bool>> UpdateOperationAreaAsync(Guid id, string name)
    {
        try
        {
            await _mediator.Send(new UpdateOperationAreaRequest() { Id = id, Name = name });
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

    public async Task<ServiceResponse<bool>> DeleteOperationAreaAsync(Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteOperationAreaRequest() { Id = id });
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
