using Application.Features.Common.AX.Services;
using Application.Features.StoreManager.Events.Operations.Event.Delete.PerStore;
using Application.Features.StoreManager.Stores.Models;
using Application.Features.StoreManager.Stores.Operations.Create;
using Application.Features.StoreManager.Stores.Operations.Delete;
using Application.Features.StoreManager.Stores.Operations.Read.All;
using Application.Features.StoreManager.Stores.Operations.Read.One;
using Application.Features.StoreManager.Stores.Operations.Update;
using Application.Features.StoreManager.Technologies.Operations.Create;
using Application.Features.StoreManager.Technologies.Operations.Delete;

namespace Application.Features.StoreManager.Stores.Services;
public class StoreService : IStoreService
{
    private readonly IMediator _mediator;
    private readonly IAxService _axService;

    public StoreService(IMediator mediator, IAxService axService)
    {
        _mediator = mediator;
        _axService = axService;
    }

    public async Task<ServiceResponse<List<Store>>> GetStoresAsync()
    {
        try
        {
            var result = await _mediator.Send(new GetStoresRequest());
            if (result == null)
            {
                return new ServiceResponse<List<Store>>
                {
                    Success = false,
                    Message = "Es ist ein Fehler beim Holen der Filiale aufgetreten."
                };
            }

            var stores = StoreMapper.GetStoresFromGetStoresReturn(result);
            return new ServiceResponse<List<Store>> { Data = stores };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<Store>>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<Store>> GetStoreAsync(string storeId)
    {
        try
        {
            var result = await _mediator.Send(new GetStoreRequest { StoreId = storeId });
            if (result == null)
            {
                return new ServiceResponse<Store>
                {
                    Success = false,
                    Message = $"Keine Filiale mit der Nr. '{storeId}' gefunden."
                };
            }

            var store = StoreMapper.GetStoreReturnToStore(result);
            return new ServiceResponse<Store> { Data = store };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<Store>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<string>> CreateStoreAsync(string storeId)
    {
        try
        {
            // check store id
            var resultStoreValid = await _axService.CheckStoreId(storeId);
            if (resultStoreValid == null || !resultStoreValid.Success)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = $"Fehler beim Überprüfen der Filialnummer."
                };
            }
            else if (!resultStoreValid.Data)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = $"Diese Filiale ist nicht in AX angelegt."
                };
            }

            // get store description
            var storeDescription = string.Empty;
            var resultAx = await _axService.GetStoreDescription(storeId);
            if (resultAx != null && resultAx.Success && !string.IsNullOrEmpty(resultAx.Data))
            {
                storeDescription = resultAx.Data;
            }

            // add store model to db
            var resultStore = await _mediator.Send(new CreateStoreRequest() { StoreId = storeId, Description = storeDescription });
            if (string.IsNullOrEmpty(resultStore))
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = $"Fehler beim Erstellen der Filiale."
                };
            }

            // create db entry for technology
            var resultTechnology = await _mediator.Send(
                new CreateTechnologyRequest
                {
                    StoreId = storeId,
                    CashDeskName = $"FIL00{resultStore}01",
                    InternetCustomerId = "1780000638"
                });
            if (string.IsNullOrEmpty(resultTechnology))
            {
                await _mediator.Send(new DeleteStoreRequest { StoreId = resultStore });

                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = $"Fehler beim Erstellen der Filial-Technologie."
                };
            }

            return new ServiceResponse<string> { Data = resultStore };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<string>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<bool>> UpdateStoreAsync(string storeId, string description)
    {
        try
        {
            await _mediator.Send(new UpdateStoreRequest()
            {
                StoreId = storeId,
                Description = description,
            });

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

    public async Task<ServiceResponse<bool>> DeleteStoreAsync(string storeId)
    {
        try
        {
            var resultTechnology = await _mediator.Send(new DeleteTechnologyRequest { StoreId = storeId });
            var resultEvents = await _mediator.Send(new DeleteEventsStoreRequest { StoreId = storeId });
            var resultStore = await _mediator.Send(new DeleteStoreRequest() { StoreId = storeId });

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
