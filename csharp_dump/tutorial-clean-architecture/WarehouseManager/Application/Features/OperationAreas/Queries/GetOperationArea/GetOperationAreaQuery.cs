namespace Application.Features.OperationAreas.Queries.GetOperationArea;
public class GetOperationAreaQuery : IRequest<GetOperationAreaVm>
{
    public Guid OperationAreaId { get; set; }
}
