using Application.Features.WarehouseManager.OperationAreas.Models;
using Application.Features.WarehouseManager.OperationAreas.Repositories;

namespace Application.Features.WarehouseManager.OperationAreas.Operations.Update;
public class UpdateOperationAreaHandler : IRequestHandler<UpdateOperationAreaRequest, Unit>
{
    private readonly IOperationAreaRepository _operationAreasRepository;

    public UpdateOperationAreaHandler(IOperationAreaRepository operationAreaRepository)
    {
        _operationAreasRepository = operationAreaRepository;
    }

    public async Task<Unit> Handle(UpdateOperationAreaRequest request, CancellationToken cancellationToken)
    {
        var operationArea = await _operationAreasRepository.GetByIdAsync(request.Id);
        if (operationArea == null)
            throw new NotFoundException(nameof(OperationArea), request.Id);

        var validator = new UpdateOperationAreaValidator(_operationAreasRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        OperationAreaMapper.UpdateOperationAreaRequestToOperationArea(request, operationArea);
        operationArea.LastModifiedDate = DateTime.Now;
        await _operationAreasRepository.UpdateAsync(operationArea);

        return Unit.Value;
    }
}
