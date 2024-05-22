using Application.Features.WarehouseManager.Articles.Repositories;
using Application.Features.WarehouseManager.Procurements.Models;
using Application.Features.WarehouseManager.Procurements.Repositories;

namespace Application.Features.WarehouseManager.Procurements.Operations.Update;
public class UpdateProcurementHandler : IRequestHandler<UpdateProcurementRequest, Unit>
{
    private readonly IProcurementRepository _procurementRepository;
    private readonly IArticleRepository _articleRepository;

    public UpdateProcurementHandler(IProcurementRepository procurementRepository, IArticleRepository articleRepository)
    {
        _procurementRepository = procurementRepository;
        _articleRepository = articleRepository;
    }

    public async Task<Unit> Handle(UpdateProcurementRequest request, CancellationToken cancellationToken)
    {
        var procurement = await _procurementRepository.GetByIdAsync(request.Id);
        if (procurement == null)
            throw new NotFoundException(nameof(Procurement), request.Id);

        var match = await _articleRepository.FindAnyArticleWithEntityId(a => a.GroupId == request.Id);
        if (match)
            throw new InUseException(nameof(Procurement), request.Id);

        var validator = new UpdateProcurementValidator(_procurementRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        ProcurementMapper.UpdateProcurementRequestToProcurement(request, procurement);
        procurement.LastModifiedDate = DateTime.Now;
        await _procurementRepository.UpdateAsync(procurement);

        return Unit.Value;
    }
}
