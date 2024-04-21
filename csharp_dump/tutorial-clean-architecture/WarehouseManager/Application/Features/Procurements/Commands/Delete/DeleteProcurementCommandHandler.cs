namespace Application.Features.Procurements.Commands.Delete;
public class DeleteProcurementCommandHandler : IRequestHandler<DeleteProcurementCommand, Unit>
{
    private readonly IProcurementRepository _procurementRepository;
    private readonly IArticleRepository _articleRepository;

    public DeleteProcurementCommandHandler(IProcurementRepository procurementRepository, IArticleRepository articleRepository)
    {
        _procurementRepository = procurementRepository;
        _articleRepository = articleRepository;
    }

    public async Task<Unit> Handle(DeleteProcurementCommand request, CancellationToken cancellationToken)
    {
        var procurement = await _procurementRepository.GetByIdAsync(request.ProcurementId);
        if (procurement == null)
            throw new NotFoundException(nameof(Procurement), request.ProcurementId);

        var match = await _articleRepository.FindAnyArticleWithEntityId(a => a.ProcurementId == request.ProcurementId);
        if (match)
            throw new InUseException(nameof(Group), request.ProcurementId);

        await _procurementRepository.DeleteAsync(procurement);

        return Unit.Value;
    }
}
