namespace Application.Features.OperationAreas.Commands.Delete;
public class DeleteOperationAreaCommandHandler : IRequestHandler<DeleteOperationAreaCommand, Unit>
{
    private readonly IOperationAreaRepository _operationAreaRepository;
    private readonly IArticleRepository _articleRepository;

    public DeleteOperationAreaCommandHandler(IOperationAreaRepository operationAreaRepository, IArticleRepository articleRepository)
    {
        _operationAreaRepository = operationAreaRepository;
        _articleRepository = articleRepository;
    }

    public async Task<Unit> Handle(DeleteOperationAreaCommand request, CancellationToken cancellationToken)
    {
        var operationAreas = await _operationAreaRepository.GetByIdAsync(request.OperationAreaId);
        if (operationAreas == null)
            throw new NotFoundException(nameof(OperationArea), request.OperationAreaId);

        var match = await _articleRepository.FindAnyArticleWithEntityId(a => a.OperationAreaId == request.OperationAreaId);
        if (match)
            throw new InUseException(nameof(Group), request.OperationAreaId);

        await _operationAreaRepository.DeleteAsync(operationAreas);

        return Unit.Value;
    }
}
