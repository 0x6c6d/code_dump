namespace Application.Features.OperationAreas.Commands.Create;
public class CreateOperationAreaCommandHandler : IRequestHandler<CreateOperationAreaCommand, Guid>
{
    private readonly IOperationAreaRepository _operationAreasRepository;

    public CreateOperationAreaCommandHandler(IOperationAreaRepository operationAreasRepository)
    {
        _operationAreasRepository = operationAreasRepository;
    }

    public async Task<Guid> Handle(CreateOperationAreaCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateOperationAreaCommandValidator(_operationAreasRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validationResult);

        var operationArea = Mappers.CreateOperationAreaCommandToOperationArea(request);
        operationArea.CreatedDate = DateTime.Now;
        operationArea.LastModifiedDate = DateTime.Now;
        operationArea = await _operationAreasRepository.AddAsync(operationArea);

        return operationArea.OperationAreaId;
    }
}
