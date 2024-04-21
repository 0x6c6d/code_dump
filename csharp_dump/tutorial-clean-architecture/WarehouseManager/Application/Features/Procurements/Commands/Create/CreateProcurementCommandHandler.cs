namespace Application.Features.Procurements.Commands.Create;
public class CreateProcurementCommandHandler : IRequestHandler<CreateProcurementCommand, Guid>
{
    private readonly IProcurementRepository _procurementRepository;

    public CreateProcurementCommandHandler(IProcurementRepository procurementRepository)
    {
        _procurementRepository = procurementRepository;
    }

    public async Task<Guid> Handle(CreateProcurementCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateProcurementCommandValidator(_procurementRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validationResult);

        var procurement = Mappers.CreateProcurementCommandToAProcurement(request);
        procurement.CreatedDate = DateTime.Now;
        procurement.LastModifiedDate = DateTime.Now;
        procurement = await _procurementRepository.AddAsync(procurement);

        return procurement.ProcurementId;
    }
}
