namespace Application.Features.WarehouseManager.StoragePlaces.Operations.Read.One;
public class GetStoragePlaceRequest : IRequest<GetStoragePlaceReturn>
{
    public Guid Id { get; set; }
}
