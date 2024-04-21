﻿namespace Application.Features.Articles.Commands.Create;
public class CreateArticleCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string ItemNumber { get; set; } = string.Empty;
    public string StorageBin { get; set; } = string.Empty;
    public Guid GroupId { get; set; }
    public Guid OperationAreaId { get; set; }
    public Guid StoragePlaceId { get; set; }
    public Guid ProcurementId { get; set; }
    public int Stock { get; set; }
    public int MinStock { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string ShoppingUrl { get; set; } = string.Empty;
}
