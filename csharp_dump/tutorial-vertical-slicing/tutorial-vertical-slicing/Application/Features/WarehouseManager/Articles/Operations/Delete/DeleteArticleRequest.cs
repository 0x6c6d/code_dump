namespace Application.Features.WarehouseManager.Articles.Operations.Delete;
public class DeleteArticleRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
}
