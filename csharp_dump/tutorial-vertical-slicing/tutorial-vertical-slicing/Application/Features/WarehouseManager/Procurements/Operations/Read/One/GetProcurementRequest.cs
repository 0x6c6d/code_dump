namespace Application.Features.WarehouseManager.Procurements.Operations.Read.One;
public class GetProcurementRequest : IRequest<GetProcurementReturn>
{
    public Guid Id { get; set; }
}
