namespace Application.Features.WarehouseManager.Procurements.Operations.Update;
public class UpdateProcurementRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public string SalesName { get; set; } = string.Empty;
    public string SalesEmail { get; set; } = string.Empty;
    public string SalesPhone { get; set; } = string.Empty;
    public string SupportName { get; set; } = string.Empty;
    public string SupportEmail { get; set; } = string.Empty;
    public string SupportPhone { get; set; } = string.Empty;
}
