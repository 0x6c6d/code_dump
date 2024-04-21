namespace Application.Features.Procurements.Queries.GetProcurements;
public class GetProcurementsQueryHandler : IRequestHandler<GetProcurementsQuery, List<GetProcurementsVm>>
{
    private readonly IProcurementRepository _procurementRepository;

    public GetProcurementsQueryHandler(IProcurementRepository procurementRepository)
    {
        _procurementRepository = procurementRepository;
    }

    public async Task<List<GetProcurementsVm>> Handle(GetProcurementsQuery request, CancellationToken cancellationToken)
    {
        var procurements = (await _procurementRepository.GetAllAsync()).OrderBy(u => u.Name);

        var procurementsVm = Mappers.ProcurementToGetProcurementsVm(procurements);

        return procurementsVm;
    }
}
