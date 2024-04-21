namespace Application.Features.Procurements.Queries.GetProcurement;
public class GetProcurementQueryHandler : IRequestHandler<GetProcurementQuery, GetProcurementVm>
{
    private readonly IProcurementRepository _procurementRepository;

    public GetProcurementQueryHandler(IProcurementRepository procurementRepository)
    {
        _procurementRepository = procurementRepository;
    }

    public async Task<GetProcurementVm> Handle(GetProcurementQuery request, CancellationToken cancellationToken)
    {
        var procurement = await _procurementRepository.GetByIdAsync(request.ProcurementId);

        if (procurement == null)
            throw new NotFoundException(nameof(Procurement), request.ProcurementId);

        var procurementVm = Mappers.ProcurementToGetProcurementVm(procurement);

        return procurementVm;
    }
}
