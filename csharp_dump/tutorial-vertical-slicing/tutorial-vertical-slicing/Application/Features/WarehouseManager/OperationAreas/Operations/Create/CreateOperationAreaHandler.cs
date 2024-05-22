using Application.Features.WarehouseManager.OperationAreas.Repositories;

namespace Application.Features.WarehouseManager.OperationAreas.Operations.Create;
public class CreateOperationAreaHandler : IRequestHandler<CreateOperationAreaRequest, Guid>
{
    private readonly IOperationAreaRepository _operationAreasRepository;

    public CreateOperationAreaHandler(IOperationAreaRepository operationAreasRepository)
    {
        _operationAreasRepository = operationAreasRepository;
    }

    public async Task<Guid> Handle(CreateOperationAreaRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateOperationAreaValidator(_operationAreasRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        var operationArea = OperationAreaMapper.CreateOperationAreaRequestToOperationArea(request);
        operationArea.CreatedDate = DateTime.Now;
        operationArea.LastModifiedDate = DateTime.Now;
        operationArea = await _operationAreasRepository.AddAsync(operationArea);

        return operationArea.Id;
    }
}
