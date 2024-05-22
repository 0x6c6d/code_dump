using Application.Features.WarehouseManager.OperationAreas.Models;
using Application.Features.WarehouseManager.OperationAreas.Repositories;

namespace Application.Features.WarehouseManager.OperationAreas.Operations.Read.One;
public class GetOperationAreaHandler : IRequestHandler<GetOperationAreaRequest, GetOperationAreaReturn>
{
    private readonly IOperationAreaRepository _operationAreaRepository;

    public GetOperationAreaHandler(IOperationAreaRepository operationAreaRepository)
    {
        _operationAreaRepository = operationAreaRepository;
    }

    public async Task<GetOperationAreaReturn> Handle(GetOperationAreaRequest request, CancellationToken cancellationToken)
    {
        var operationArea = await _operationAreaRepository.GetByIdAsync(request.Id);

        if (operationArea == null)
            throw new NotFoundException(nameof(OperationArea), request.Id);

        var operationAreaVm = OperationAreaMapper.OperationAreaToGetOperationAreaReturn(operationArea);

        return operationAreaVm;
    }
}
