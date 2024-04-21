namespace Application.Features.Articles.Queries.GetArticle;
public class GetArticleQuery : IRequest<GetArticleVm>
{
    public Guid ArticleId { get; set; }
}
