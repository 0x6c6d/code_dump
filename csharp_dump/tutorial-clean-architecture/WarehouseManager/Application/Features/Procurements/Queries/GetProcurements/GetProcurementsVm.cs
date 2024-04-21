namespace Application.Features.Procurements.Queries.GetProcurements;
public class GetProcurementsVm
{
    public Guid ProcurementId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}