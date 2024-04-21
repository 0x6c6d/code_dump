namespace Application.Features.OperationAreas.Commands.Update;
public class UpdateOperationAreaCommandHandler : IRequestHandler<UpdateOperationAreaCommand, Unit>
{
    private readonly IOperationAreaRepository _operationAreasRepository;

    public UpdateOperationAreaCommandHandler(IOperationAreaRepository operationAreaRepository)
    {
        _operationAreasRepository = operationAreaRepository;
    }

    public async Task<Unit> Handle(UpdateOperationAreaCommand request, CancellationToken cancellationToken)
    {
        var operationArea = await _operationAreasRepository.GetByIdAsync(request.OperationAreaId);
        if (operationArea == null)
            throw new NotFoundException(nameof(OperationArea), request.OperationAreaId);

        var validator = new UpdateOperationAreaCommandValidator(_operationAreasRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validationResult);

        Mappers.UpdateOperationAreaCommandToOperationArea(request, operationArea);
        operationArea.LastModifiedDate = DateTime.Now;
        await _operationAreasRepository.UpdateAsync(operationArea);

        return Unit.Value;
    }
}
