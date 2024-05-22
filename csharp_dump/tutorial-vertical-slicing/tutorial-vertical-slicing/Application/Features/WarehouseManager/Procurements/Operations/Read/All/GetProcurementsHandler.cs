using Application.Features.WarehouseManager.Procurements.Repositories;

namespace Application.Features.WarehouseManager.Procurements.Operations.Read.All;
public class GetProcurementsHandler : IRequestHandler<GetProcurementsRequest, List<GetProcurementsReturn>>
{
    private readonly IProcurementRepository _procurementRepository;

    public GetProcurementsHandler(IProcurementRepository procurementRepository)
    {
        _procurementRepository = procurementRepository;
    }

    public async Task<List<GetProcurementsReturn>> Handle(GetProcurementsRequest request, CancellationToken cancellationToken)
    {
        var procurements = (await _procurementRepository.GetAllAsync()).OrderBy(u => u.Name);

        var procurementsReturn = ProcurementMapper.ProcurementToGetProcurementsReturn(procurements);

        return procurementsReturn;
    }
}
