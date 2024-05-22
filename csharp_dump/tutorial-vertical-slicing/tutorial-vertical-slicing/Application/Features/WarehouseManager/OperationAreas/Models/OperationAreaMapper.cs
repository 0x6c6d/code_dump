using Application.Features.WarehouseManager.OperationAreas.Models;
using Application.Features.WarehouseManager.OperationAreas.Operations.Create;
using Application.Features.WarehouseManager.OperationAreas.Operations.Read.All;
using Application.Features.WarehouseManager.OperationAreas.Operations.Read.One;
using Application.Features.WarehouseManager.OperationAreas.Operations.Update;
using Riok.Mapperly.Abstractions;

namespace Application.Features.WarehouseManager.OperationAreas;

[Mapper]
public static partial class OperationAreaMapper
{
    // MediatR
    public static partial GetOperationAreaReturn OperationAreaToGetOperationAreaReturn(OperationArea operationArea);

    public static partial List<GetOperationAreasReturn> OperationAreasToGetOperationAreasReturn(IOrderedEnumerable<OperationArea> operationAreas);

    public static partial OperationArea CreateOperationAreaRequestToOperationArea(CreateOperationAreaRequest createOperationAreaRequest);

    public static partial void UpdateOperationAreaRequestToOperationArea(UpdateOperationAreaRequest updateOperationAreaRequest, OperationArea operationArea);

    // Business Logic
    public static partial OperationArea GetOperationAreaFromGetOperationAreaReturn(GetOperationAreaReturn getOperationAreaReturn);

    public static partial List<OperationArea> GetOperationAreasFromGetOperationAreasReturn(List<GetOperationAreasReturn> getOperationAreasReturn);
}
