using Application.Features.WarehouseManager.Procurements.Repositories;

namespace Application.Features.WarehouseManager.Procurements.Operations.Create;
public class CreateProcurementHandler : IRequestHandler<CreateProcurementRequest, Guid>
{
    private readonly IProcurementRepository _procurementRepository;

    public CreateProcurementHandler(IProcurementRepository procurementRepository)
    {
        _procurementRepository = procurementRepository;
    }

    public async Task<Guid> Handle(CreateProcurementRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateProcurementValidator(_procurementRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        var procurement = ProcurementMapper.CreateProcurementRequestToProcurement(request);
        procurement.CreatedDate = DateTime.Now;
        procurement.LastModifiedDate = DateTime.Now;
        procurement = await _procurementRepository.AddAsync(procurement);

        return procurement.Id;
    }
}
