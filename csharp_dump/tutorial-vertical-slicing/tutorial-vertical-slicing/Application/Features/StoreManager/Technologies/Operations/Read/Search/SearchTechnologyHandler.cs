using Application.Features.StoreManager.Technologies.Models;
using Application.Features.StoreManager.Technologies.Repositories;

namespace Application.Features.StoreManager.Technologies.Operations.Read.Search;
public class SearchTechnologyHandler : IRequestHandler<SearchTechnologyRequest, List<string>>
{
    private readonly ITechnologyRepository _technologyRepository;

    public SearchTechnologyHandler(ITechnologyRepository technologyRepository)
    {
        _technologyRepository = technologyRepository;
    }

    public Task<List<string>> Handle(SearchTechnologyRequest request, CancellationToken cancellationToken)
    {
        var stores = _technologyRepository.SearchTechnologyForString(request.SearchInput);

        if (stores == null || stores.Count == 0)
            throw new NotFoundException(nameof(Technology), request.SearchInput);

        return Task.FromResult(stores);
    }
}
