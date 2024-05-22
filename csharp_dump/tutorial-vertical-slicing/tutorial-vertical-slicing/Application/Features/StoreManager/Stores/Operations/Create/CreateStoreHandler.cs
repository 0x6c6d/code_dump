using Application.Features.StoreManager.Stores.Repositories;

namespace Application.Features.StoreManager.Stores.Operations.Create;
public class CreateStoreHandler : IRequestHandler<CreateStoreRequest, string>
{
    private readonly IStoreRepository _storeRepository;

    public CreateStoreHandler(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<string> Handle(CreateStoreRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateStoreValidator(_storeRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        var store = StoreMapper.CreateStoreRequestToStore(request);
        store.CreatedDate = DateTime.Now;
        store.LastModifiedDate = DateTime.Now;
        store = await _storeRepository.AddAsync(store);

        return store.StoreId;
    }
}
