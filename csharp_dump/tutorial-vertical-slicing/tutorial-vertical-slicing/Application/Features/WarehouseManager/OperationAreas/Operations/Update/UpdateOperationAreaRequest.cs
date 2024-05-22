namespace Application.Features.WarehouseManager.OperationAreas.Operations.Update;
public class UpdateOperationAreaRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
