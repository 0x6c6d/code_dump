using Application.Features.WarehouseManager.Procurements.Models;
using Application.Features.WarehouseManager.Procurements.Operations.Delete;
using Application.Features.WarehouseManager.Procurements.Operations.Read.All;
using Application.Features.WarehouseManager.Procurements.Operations.Read.One;

namespace Application.Features.WarehouseManager.Procurements.Services;
public class ProcurementService : IProcurementService
{
    private readonly IMediator _mediator;

    public ProcurementService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ServiceResponse<Procurement>> GetProcurementAsync(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetProcurementRequest { Id = id });
            if (result == null)
            {
                return new ServiceResponse<Procurement>
                {
                    Success = false,
                    Message = $"Keinen Lieferanten mit der ID {id} gefunden."
                };
            }

            var procurement = ProcurementMapper.GetProcurementFromGetProcurementReturn(result);
            return new ServiceResponse<Procurement> { Data = procurement };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<Procurement>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<List<Procurement>>> GetProcurementsAsync()
    {
        try
        {
            var result = await _mediator.Send(new GetProcurementsRequest());
            if (result == null)
            {
                return new ServiceResponse<List<Procurement>>
                {
                    Success = false,
                    Message = "Keinen Lieferanenten gefunden."
                };
            }

            var procurements = ProcurementMapper.GetProcurementsFromGetProcurementsReturn(result);
            return new ServiceResponse<List<Procurement>> { Data = procurements };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<Procurement>>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<Guid>> CreateProcurementAsync(Procurement procurement)
    {
        try
        {
            var request = ProcurementMapper.ProcurementToCreateProcurementRequest(procurement);
            var result = await _mediator.Send(request);

            if (result == Guid.Empty)
            {
                return new ServiceResponse<Guid>
                {
                    Success = false,
                    Message = $"Fehler beim Erstellen des Lieferanten."
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

    public async Task<ServiceResponse<bool>> UpdateProcurementAsync(Procurement procurement)
    {
        try
        {
            var request = ProcurementMapper.ProcurementToUpdateProcurementRequest(procurement);
            await _mediator.Send(request);

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

    public async Task<ServiceResponse<bool>> DeleteProcurementAsync(Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteProcurementRequest() { Id = id });
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
