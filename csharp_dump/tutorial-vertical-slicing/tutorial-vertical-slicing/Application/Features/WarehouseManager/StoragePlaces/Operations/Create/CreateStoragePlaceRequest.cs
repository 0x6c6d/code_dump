namespace Application.Features.WarehouseManager.StoragePlaces.Operations.Create;
public class CreateStoragePlaceRequest : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
}
