using Application.Features.StoreManager.Technologies.Models;
using Application.Features.StoreManager.Technologies.Repositories;

namespace Application.Features.StoreManager.Technologies.Operations.Read.One;
public class GetTechnologyHandler : IRequestHandler<GetTechnologyRequest, GetTechnologyReturn>
{
    private readonly ITechnologyRepository _technologyRepository;

    public GetTechnologyHandler(ITechnologyRepository technologyRepository)
    {
        _technologyRepository = technologyRepository;
    }

    public async Task<GetTechnologyReturn> Handle(GetTechnologyRequest request, CancellationToken cancellationToken)
    {
        var technology = await _technologyRepository.GetByStoreIdAsync(request.StoreId);

        if (technology == null)
            throw new NotFoundException(nameof(Technology), request.StoreId);

        var technololgyVm = TechnologyMapper.TechnologyToGetTechnologyReturn(technology);

        return technololgyVm;
    }
}
