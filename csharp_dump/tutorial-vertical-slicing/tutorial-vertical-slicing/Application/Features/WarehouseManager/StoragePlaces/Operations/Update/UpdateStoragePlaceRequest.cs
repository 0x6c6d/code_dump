namespace Application.Features.WarehouseManager.StoragePlaces.Operations.Update;
public class UpdateStoragePlaceRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
