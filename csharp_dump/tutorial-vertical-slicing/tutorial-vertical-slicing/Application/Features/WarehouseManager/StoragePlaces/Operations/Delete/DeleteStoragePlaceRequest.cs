namespace Application.Features.WarehouseManager.StoragePlaces.Operations.Delete;
public class DeleteStoragePlaceRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
}
