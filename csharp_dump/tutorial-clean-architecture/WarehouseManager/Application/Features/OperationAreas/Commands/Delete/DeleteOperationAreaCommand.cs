namespace Application.Features.OperationAreas.Commands.Delete;
public class DeleteOperationAreaCommand : IRequest<Unit>
{
    public Guid OperationAreaId { get; set; }
}
