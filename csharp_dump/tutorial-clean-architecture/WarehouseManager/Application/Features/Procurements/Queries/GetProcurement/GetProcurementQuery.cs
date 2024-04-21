namespace Application.Features.Procurements.Queries.GetProcurement;
public class GetProcurementQuery : IRequest<GetProcurementVm>
{
    public Guid ProcurementId { get; set; }
}
