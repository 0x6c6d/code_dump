namespace Application.Features.StoragePlaces.Queries.GetStoragePlace;
public class GetStoragePlaceQueryHandler : IRequestHandler<GetStoragePlaceQuery, GetStoragePlaceVm>
{
    private readonly IStoragePlaceRepository _storagePlaceRepository;

    public GetStoragePlaceQueryHandler(IStoragePlaceRepository storagePlaceRepository)
    {
        _storagePlaceRepository = storagePlaceRepository;
    }

    public async Task<GetStoragePlaceVm> Handle(GetStoragePlaceQuery request, CancellationToken cancellationToken)
    {
        var storagePlace = await _storagePlaceRepository.GetByIdAsync(request.StoragePlaceId);

        if (storagePlace == null)
            throw new NotFoundException(nameof(StoragePlace), request.StoragePlaceId);

        var storagePlaceVm = Mappers.StoragePlaceToGetStoragePlaceVm(storagePlace);

        return storagePlaceVm;
    }
}
