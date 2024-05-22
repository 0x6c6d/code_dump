using Application.Features.WarehouseManager.Procurements.Models;
using Application.Features.WarehouseManager.Procurements.Operations.Create;
using Application.Features.WarehouseManager.Procurements.Operations.Read.All;
using Application.Features.WarehouseManager.Procurements.Operations.Read.One;
using Application.Features.WarehouseManager.Procurements.Operations.Update;
using Riok.Mapperly.Abstractions;

namespace Application.Features.WarehouseManager.Procurements;

[Mapper]
public static partial class ProcurementMapper
{
    // MediatR
    public static partial GetProcurementReturn ProcurementToGetProcurementReturn(Procurement procurement);

    public static partial List<GetProcurementsReturn> ProcurementToGetProcurementsReturn(IOrderedEnumerable<Procurement> procurements);

    public static partial Procurement CreateProcurementRequestToProcurement(CreateProcurementRequest request);

    public static partial void UpdateProcurementRequestToProcurement(UpdateProcurementRequest request, Procurement procurement);

    // Business Logic
    public static partial Procurement GetProcurementFromGetProcurementReturn(GetProcurementReturn getProcurementReturn);

    public static partial List<Procurement> GetProcurementsFromGetProcurementsReturn(List<GetProcurementsReturn> getProcurementsReturn);

    public static partial CreateProcurementRequest ProcurementToCreateProcurementRequest(Procurement procurement);

    public static partial UpdateProcurementRequest ProcurementToUpdateProcurementRequest(Procurement procurement);
}
