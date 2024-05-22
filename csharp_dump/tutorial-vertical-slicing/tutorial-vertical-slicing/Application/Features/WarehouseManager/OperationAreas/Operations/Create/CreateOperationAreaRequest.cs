namespace Application.Features.WarehouseManager.OperationAreas.Operations.Create;
public class CreateOperationAreaRequest : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
}
