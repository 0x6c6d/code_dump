namespace Application.Features.StoragePlaces.Commands.Update;
public class UpdateStoragePlaceCommandHandler : IRequestHandler<UpdateStoragePlaceCommand, Unit>
{
    private readonly IStoragePlaceRepository _storagePlaceRepository;

    public UpdateStoragePlaceCommandHandler(IStoragePlaceRepository storagePlaceRepository)
    {
        _storagePlaceRepository = storagePlaceRepository;
    }

    public async Task<Unit> Handle(UpdateStoragePlaceCommand request, CancellationToken cancellationToken)
    {
        var storagePlace = await _storagePlaceRepository.GetByIdAsync(request.StoragePlaceId);
        if (storagePlace == null)
            throw new NotFoundException(nameof(StoragePlace), request.StoragePlaceId);

        var validator = new UpdateStoragePlaceCommandValidator(_storagePlaceRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validationResult);

        Mappers.UpdateStoragePlaceCommandToStoragePlace(request, storagePlace);
        storagePlace.LastModifiedDate = DateTime.Now;
        await _storagePlaceRepository.UpdateAsync(storagePlace);

        return Unit.Value;
    }
}
