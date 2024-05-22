namespace Application.Features.WarehouseManager.Articles.Operations.Read.One;
public class GetArticleRequest : IRequest<GetArticleReturn>
{
    public Guid Id { get; set; }
}
