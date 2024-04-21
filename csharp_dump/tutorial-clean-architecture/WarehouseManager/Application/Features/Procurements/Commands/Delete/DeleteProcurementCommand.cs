namespace Application.Features.Procurements.Commands.Delete;
public class DeleteProcurementCommand : IRequest<Unit>
{
    public Guid ProcurementId { get; set; }
}