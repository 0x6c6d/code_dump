﻿namespace Application.Features.Procurements.Queries.GetProcurement;
public class GetProcurementVm
{
    public Guid ProcurementId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}
