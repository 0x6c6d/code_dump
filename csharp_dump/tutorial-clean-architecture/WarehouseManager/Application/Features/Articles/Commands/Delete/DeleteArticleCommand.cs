namespace Application.Features.Articles.Commands.Delete;
public class DeleteArticleCommand : IRequest<Unit>
{
    public Guid ArticleId { get; set; }
}
