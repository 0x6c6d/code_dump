namespace Application.Features.Procurements.Commands.Update;
public class UpdateProcurementCommandHandler : IRequestHandler<UpdateProcurementCommand, Unit>
{
    private readonly IProcurementRepository _procurementRepository;
    private readonly IArticleRepository _articleRepository;

    public UpdateProcurementCommandHandler(IProcurementRepository procurementRepository, IArticleRepository articleRepository)
    {
        _procurementRepository = procurementRepository;
        _articleRepository = articleRepository;
    }

    public async Task<Unit> Handle(UpdateProcurementCommand request, CancellationToken cancellationToken)
    {
        var procurement = await _procurementRepository.GetByIdAsync(request.ProcurementId);
        if (procurement == null)
            throw new NotFoundException(nameof(Procurement), request.ProcurementId);

        var match = await _articleRepository.FindAnyArticleWithEntityId(a => a.GroupId == request.ProcurementId);
        if (match)
            throw new InUseException(nameof(Group), request.ProcurementId);

        var validator = new UpdateProcurementCommandValidator(_procurementRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validationResult);

        Mappers.UpdateProcurementCommandToProcurement(request, procurement);
        procurement.LastModifiedDate = DateTime.Now;
        await _procurementRepository.UpdateAsync(procurement);

        return Unit.Value;
    }
}
