namespace Application.Features.StoragePlaces.Queries.GetStoragePlaces;
public class GetStoragePlacesQueryHandler : IRequestHandler<GetStoragePlacesQuery, List<GetStoragePlacesVm>>
{
    private readonly IStoragePlaceRepository _storagePlaceRepository;

    public GetStoragePlacesQueryHandler(IStoragePlaceRepository storagePlaceRepository)
    {
        _storagePlaceRepository = storagePlaceRepository;
    }

    public async Task<List<GetStoragePlacesVm>> Handle(GetStoragePlacesQuery request, CancellationToken cancellationToken)
    {
        var storagePlaces = (await _storagePlaceRepository.GetAllAsync()).OrderBy(u => u.Name);

        var storagePlacesVm = Mappers.StoragePlacesToGetStoragePlacesVm(storagePlaces);

        return storagePlacesVm;
    }
}
