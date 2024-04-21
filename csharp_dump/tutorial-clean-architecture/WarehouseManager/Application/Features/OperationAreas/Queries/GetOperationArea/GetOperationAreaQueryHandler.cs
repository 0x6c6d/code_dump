namespace Application.Features.OperationAreas.Queries.GetOperationArea;
public class GetOperationAreaQueryHandler : IRequestHandler<GetOperationAreaQuery, GetOperationAreaVm>
{
    private readonly IOperationAreaRepository _operationAreaRepository;

    public GetOperationAreaQueryHandler(IOperationAreaRepository operationAreaRepository)
    {
        _operationAreaRepository = operationAreaRepository;
    }

    public async Task<GetOperationAreaVm> Handle(GetOperationAreaQuery request, CancellationToken cancellationToken)
    {
        var operationArea = await _operationAreaRepository.GetByIdAsync(request.OperationAreaId);

        if (operationArea == null)
            throw new NotFoundException(nameof(OperationArea), request.OperationAreaId);

        var operationAreaVm = Mappers.OperationAreaToGetOperationAreaVm(operationArea);

        return operationAreaVm;
    }
}
