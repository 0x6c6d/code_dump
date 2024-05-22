using Application.Features.WarehouseManager.Articles.Repositories;
using Application.Features.WarehouseManager.Procurements.Models;
using Application.Features.WarehouseManager.Procurements.Repositories;

namespace Application.Features.WarehouseManager.Procurements.Operations.Delete;
public class DeleteProcurementHandler : IRequestHandler<DeleteProcurementRequest, Unit>
{
    private readonly IProcurementRepository _procurementRepository;
    private readonly IArticleRepository _articleRepository;

    public DeleteProcurementHandler(IProcurementRepository procurementRepository, IArticleRepository articleRepository)
    {
        _procurementRepository = procurementRepository;
        _articleRepository = articleRepository;
    }

    public async Task<Unit> Handle(DeleteProcurementRequest request, CancellationToken cancellationToken)
    {
        var procurement = await _procurementRepository.GetByIdAsync(request.Id);
        if (procurement == null)
            throw new NotFoundException(nameof(Procurement), request.Id);

        var match = await _articleRepository.FindAnyArticleWithEntityId(a => a.ProcurementId == request.Id);
        if (match)
            throw new InUseException(nameof(Procurement), request.Id);

        await _procurementRepository.DeleteAsync(procurement);

        return Unit.Value;
    }
}
