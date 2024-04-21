namespace Application.Features.StoragePlaces.Commands.Create;
public class CreateStoragePlaceCommandHandler : IRequestHandler<CreateStoragePlaceCommand, Guid>
{
    private readonly IStoragePlaceRepository _storagePlaceRepository;

    public CreateStoragePlaceCommandHandler(IStoragePlaceRepository storagePlaceRepository)
    {
        _storagePlaceRepository = storagePlaceRepository;
    }

    public async Task<Guid> Handle(CreateStoragePlaceCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateStoragePlaceCommandValidator(_storagePlaceRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validationResult);

        var storagePlace = Mappers.CreateStoragePlaceCommandToStoragePlace(request);
        storagePlace.CreatedDate = DateTime.Now;
        storagePlace.LastModifiedDate = DateTime.Now;
        storagePlace = await _storagePlaceRepository.AddAsync(storagePlace);

        return storagePlace.StoragePlaceId;
    }
}
