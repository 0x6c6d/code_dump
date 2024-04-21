namespace Application.Features.StoragePlaces.Queries.GetStoragePlace;
public class GetStoragePlaceQuery : IRequest<GetStoragePlaceVm>
{
    public Guid StoragePlaceId { get; set; }
}
