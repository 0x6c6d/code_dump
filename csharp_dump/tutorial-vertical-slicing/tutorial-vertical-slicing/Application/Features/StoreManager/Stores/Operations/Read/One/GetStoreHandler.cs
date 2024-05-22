using Application.Features.StoreManager.Stores.Models;
using Application.Features.StoreManager.Stores.Repositories;

namespace Application.Features.StoreManager.Stores.Operations.Read.One;
public class GetStoreHandler : IRequestHandler<GetStoreRequest, GetStoreReturn>
{
    private readonly IStoreRepository _storeRepository;

    public GetStoreHandler(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<GetStoreReturn> Handle(GetStoreRequest request, CancellationToken cancellationToken)
    {
        var store = await _storeRepository.GetByStoreIdAsync(request.StoreId);

        if (store == null)
            throw new NotFoundException(nameof(Store), request.StoreId);

        var storeVm = StoreMapper.StoreToGetStoreReturn(store);

        return storeVm;
    }
}
