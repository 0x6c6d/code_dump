using Application.Features.WarehouseManager.Procurements.Models;
using Application.Features.WarehouseManager.Procurements.Repositories;

namespace Application.Features.WarehouseManager.Procurements.Operations.Read.One;
public class GetProcurementHandler : IRequestHandler<GetProcurementRequest, GetProcurementReturn>
{
    private readonly IProcurementRepository _procurementRepository;

    public GetProcurementHandler(IProcurementRepository procurementRepository)
    {
        _procurementRepository = procurementRepository;
    }

    public async Task<GetProcurementReturn> Handle(GetProcurementRequest request, CancellationToken cancellationToken)
    {
        var procurement = await _procurementRepository.GetByIdAsync(request.Id);

        if (procurement == null)
            throw new NotFoundException(nameof(Procurement), request.Id);

        var procurementReturn = ProcurementMapper.ProcurementToGetProcurementReturn(procurement);

        return procurementReturn;
    }
}
