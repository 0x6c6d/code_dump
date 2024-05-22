using Application.Features.StoreManager.Stores.Models;
using Application.Features.StoreManager.Stores.Repositories;

namespace Application.Features.StoreManager.Stores.Operations.Update;
public class UpdateStoreHandler : IRequestHandler<UpdateStoreRequest, Unit>
{
    private readonly IStoreRepository _storeRepository;

    public UpdateStoreHandler(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<Unit> Handle(UpdateStoreRequest request, CancellationToken cancellationToken)
    {
        var store = await _storeRepository.GetByStoreIdAsync(request.StoreId);
        if (store == null)
            throw new NotFoundException(nameof(Store), request.StoreId);

        var validator = new UpdateStoreValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        StoreMapper.UpdateStoreRequestToStore(request, store);
        store.LastModifiedDate = DateTime.Now;
        await _storeRepository.UpdateAsync(store);

        return Unit.Value;
    }
}
