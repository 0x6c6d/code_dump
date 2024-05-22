using Application.Features.WarehouseManager.StoragePlaces.Models;
using Application.Features.WarehouseManager.StoragePlaces.Repositories;

namespace Application.Features.WarehouseManager.StoragePlaces.Operations.Read.One;
public class GetStoragePlaceHandler : IRequestHandler<GetStoragePlaceRequest, GetStoragePlaceReturn>
{
    private readonly IStoragePlaceRepository _storagePlaceRepository;

    public GetStoragePlaceHandler(IStoragePlaceRepository storagePlaceRepository)
    {
        _storagePlaceRepository = storagePlaceRepository;
    }

    public async Task<GetStoragePlaceReturn> Handle(GetStoragePlaceRequest request, CancellationToken cancellationToken)
    {
        var storagePlace = await _storagePlaceRepository.GetByIdAsync(request.Id);

        if (storagePlace == null)
            throw new NotFoundException(nameof(StoragePlace), request.Id);

        var storagePlaceReturn = StoragePlaceMapper.StoragePlaceToGetStoragePlaceReturn(storagePlace);

        return storagePlaceReturn;
    }
}
