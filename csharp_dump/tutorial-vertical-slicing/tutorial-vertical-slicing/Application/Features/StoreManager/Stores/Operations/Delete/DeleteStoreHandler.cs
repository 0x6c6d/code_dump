using Application.Features.StoreManager.Stores.Models;
using Application.Features.StoreManager.Stores.Repositories;

namespace Application.Features.StoreManager.Stores.Operations.Delete;
public class DeleteStoreHandler : IRequestHandler<DeleteStoreRequest, Unit>
{
    private readonly IStoreRepository _storeRepository;

    public DeleteStoreHandler(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<Unit> Handle(DeleteStoreRequest request, CancellationToken cancellationToken)
    {
        var group = await _storeRepository.GetByStoreIdAsync(request.StoreId);
        if (group == null)
            throw new NotFoundException(nameof(Store), request.StoreId);

        await _storeRepository.DeleteAsync(group);

        return Unit.Value;
    }
}
