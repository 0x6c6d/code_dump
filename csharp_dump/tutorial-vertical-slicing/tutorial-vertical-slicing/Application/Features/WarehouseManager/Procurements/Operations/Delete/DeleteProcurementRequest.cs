namespace Application.Features.WarehouseManager.Procurements.Operations.Delete;
public class DeleteProcurementRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
}