using Application.Features.WarehouseManager.OperationAreas.Repositories;

namespace Application.Features.WarehouseManager.OperationAreas.Operations.Read.All;
public class GetOperationAreasHandler : IRequestHandler<GetOperationAreasRequest, List<GetOperationAreasReturn>>
{
    private readonly IOperationAreaRepository _operationAreaRepository;

    public GetOperationAreasHandler(IOperationAreaRepository operationAreaRepository)
    {
        _operationAreaRepository = operationAreaRepository;
    }

    public async Task<List<GetOperationAreasReturn>> Handle(GetOperationAreasRequest request, CancellationToken cancellationToken)
    {
        var operationAreas = (await _operationAreaRepository.GetAllAsync()).OrderBy(u => u.Name);

        var operationAreasVm = OperationAreaMapper.OperationAreasToGetOperationAreasReturn(operationAreas);

        return operationAreasVm;
    }
}
