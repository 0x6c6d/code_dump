using Application.Features.WarehouseManager.StoragePlaces.Repositories;

namespace Application.Features.WarehouseManager.StoragePlaces.Operations.Create;
public class CreateStoragePlaceHandler : IRequestHandler<CreateStoragePlaceRequest, Guid>
{
    private readonly IStoragePlaceRepository _storagePlaceRepository;

    public CreateStoragePlaceHandler(IStoragePlaceRepository storagePlaceRepository)
    {
        _storagePlaceRepository = storagePlaceRepository;
    }

    public async Task<Guid> Handle(CreateStoragePlaceRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateStoragePlaceValidator(_storagePlaceRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        var storagePlace = StoragePlaceMapper.CreateStoragePlaceRequestToStoragePlace(request);
        storagePlace.CreatedDate = DateTime.Now;
        storagePlace.LastModifiedDate = DateTime.Now;
        storagePlace = await _storagePlaceRepository.AddAsync(storagePlace);

        return storagePlace.Id;
    }
}
