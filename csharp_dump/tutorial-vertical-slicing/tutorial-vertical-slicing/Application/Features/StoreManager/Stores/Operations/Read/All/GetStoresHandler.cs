using Application.Features.StoreManager.Stores.Repositories;

namespace Application.Features.StoreManager.Stores.Operations.Read.All;
public class GetStoresHandler : IRequestHandler<GetStoresRequest, List<GetStoresReturn>>
{
    private readonly IStoreRepository _storeRepository;

    public GetStoresHandler(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<List<GetStoresReturn>> Handle(GetStoresRequest request, CancellationToken cancellationToken)
    {
        var stores = (await _storeRepository.GetAllAsync()).OrderBy(u => u.StoreId);

        var storesVm = StoreMapper.StoreToGetStoresReturn(stores);

        return storesVm;
    }
}
