namespace Application.Features.WarehouseManager.OperationAreas.Operations.Read.One;
public class GetOperationAreaRequest : IRequest<GetOperationAreaReturn>
{
    public Guid Id { get; set; }
}
