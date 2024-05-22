using Application.Features.StoreManager.Technologies.Models;
using Application.Features.StoreManager.Technologies.Operations.Read.One;
using Application.Features.StoreManager.Technologies.Operations.Read.Search;

namespace Application.Features.StoreManager.Technologies.Services;
public class TechnologyService : ITechnologyService
{
    private readonly IMediator _mediator;

    public TechnologyService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ServiceResponse<Technology>> GetTechnologyAsync(string storeId)
    {
        try
        {
            var result = await _mediator.Send(new GetTechnologyRequest { StoreId = storeId });
            if (result == null)
            {
                return new ServiceResponse<Technology>
                {
                    Success = false,
                    Message = $"Keine Technologie mit der Nr. '{storeId}' gefunden."
                };
            }

            var technology = TechnologyMapper.GetTechnologyReturnToTechnology(result);
            return new ServiceResponse<Technology> { Data = technology };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<Technology>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<bool>> UpdateTechnologyAsync(Technology technology)
    {
        try
        {
            var request = TechnologyMapper.TechnologyToUpdateTechnologyRequest(technology);
            var result = await _mediator.Send(request);

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

    public async Task<ServiceResponse<List<string>>> SearchTechnologyForString(string searchInput)
    {
        try
        {
            var result = await _mediator.Send(new SearchTechnologyRequest { SearchInput = searchInput });
            return new ServiceResponse<List<string>> { Data = result };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<string>>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
}
