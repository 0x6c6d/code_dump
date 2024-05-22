using Application.Features.WarehouseManager.StoragePlaces.Repositories;

namespace Application.Features.WarehouseManager.StoragePlaces.Operations.Read.All;
public class GetStoragePlacesHandler : IRequestHandler<GetStoragePlacesRequest, List<GetStoragePlacesReturn>>
{
    private readonly IStoragePlaceRepository _storagePlaceRepository;

    public GetStoragePlacesHandler(IStoragePlaceRepository storagePlaceRepository)
    {
        _storagePlaceRepository = storagePlaceRepository;
    }

    public async Task<List<GetStoragePlacesReturn>> Handle(GetStoragePlacesRequest request, CancellationToken cancellationToken)
    {
        var storagePlaces = (await _storagePlaceRepository.GetAllAsync()).OrderBy(u => u.Name);

        var storagePlacesReturn = StoragePlaceMapper.StoragePlacesToGetStoragePlacesReturn(storagePlaces);

        return storagePlacesReturn;
    }
}
