using Application.Features.WarehouseManager.Articles.Repositories;
using Application.Features.WarehouseManager.OperationAreas.Models;
using Application.Features.WarehouseManager.OperationAreas.Repositories;

namespace Application.Features.WarehouseManager.OperationAreas.Operations.Delete;
public class DeleteOperationAreaHandler : IRequestHandler<DeleteOperationAreaRequest, Unit>
{
    private readonly IOperationAreaRepository _operationAreaRepository;
    private readonly IArticleRepository _articleRepository;

    public DeleteOperationAreaHandler(IOperationAreaRepository operationAreaRepository, IArticleRepository articleRepository)
    {
        _operationAreaRepository = operationAreaRepository;
        _articleRepository = articleRepository;
    }

    public async Task<Unit> Handle(DeleteOperationAreaRequest request, CancellationToken cancellationToken)
    {
        var operationAreas = await _operationAreaRepository.GetByIdAsync(request.Id);
        if (operationAreas == null)
            throw new NotFoundException(nameof(OperationArea), request.Id);

        var match = await _articleRepository.FindAnyArticleWithEntityId(a => a.OperationAreaId == request.Id);
        if (match)
            throw new InUseException(nameof(OperationArea), request.Id);

        await _operationAreaRepository.DeleteAsync(operationAreas);

        return Unit.Value;
    }
}
