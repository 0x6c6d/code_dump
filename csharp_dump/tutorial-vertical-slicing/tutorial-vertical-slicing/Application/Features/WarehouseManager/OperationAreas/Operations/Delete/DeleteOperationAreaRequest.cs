namespace Application.Features.WarehouseManager.OperationAreas.Operations.Delete;
public class DeleteOperationAreaRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
}
