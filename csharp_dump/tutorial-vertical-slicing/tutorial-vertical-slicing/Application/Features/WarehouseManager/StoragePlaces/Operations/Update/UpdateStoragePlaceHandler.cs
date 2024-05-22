using Application.Features.WarehouseManager.StoragePlaces.Models;
using Application.Features.WarehouseManager.StoragePlaces.Repositories;

namespace Application.Features.WarehouseManager.StoragePlaces.Operations.Update;
public class UpdateStoragePlaceHandler : IRequestHandler<UpdateStoragePlaceRequest, Unit>
{
    private readonly IStoragePlaceRepository _storagePlaceRepository;

    public UpdateStoragePlaceHandler(IStoragePlaceRepository storagePlaceRepository)
    {
        _storagePlaceRepository = storagePlaceRepository;
    }

    public async Task<Unit> Handle(UpdateStoragePlaceRequest request, CancellationToken cancellationToken)
    {
        var storagePlace = await _storagePlaceRepository.GetByIdAsync(request.Id);
        if (storagePlace == null)
            throw new NotFoundException(nameof(StoragePlace), request.Id);

        var validator = new UpdateStoragePlaceValidator(_storagePlaceRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        StoragePlaceMapper.UpdateStoragePlaceRequestToStoragePlace(request, storagePlace);
        storagePlace.LastModifiedDate = DateTime.Now;
        await _storagePlaceRepository.UpdateAsync(storagePlace);

        return Unit.Value;
    }
}
