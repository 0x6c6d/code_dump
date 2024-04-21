namespace Application.Features.OperationAreas.Queries.GetOperationAreas;
public class GetOperationAreasQueryHandler : IRequestHandler<GetOperationAreasQuery, List<GetOperationAreasVm>>
{
    private readonly IOperationAreaRepository _operationAreaRepository;

    public GetOperationAreasQueryHandler(IOperationAreaRepository operationAreaRepository)
    {
        _operationAreaRepository = operationAreaRepository;
    }

    public async Task<List<GetOperationAreasVm>> Handle(GetOperationAreasQuery request, CancellationToken cancellationToken)
    {
        var operationAreas = (await _operationAreaRepository.GetAllAsync()).OrderBy(u => u.Name);

        var operationAreasVm = Mappers.OperationAreasToGetOperationAreasVm(operationAreas);

        return operationAreasVm;
    }
}
