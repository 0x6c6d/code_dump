namespace Application.Features.Articles.Commands.Delete;
public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, Unit>
{
    private readonly IArticleRepository _articleRepository;

    public DeleteArticleCommandHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.ArticleId);
        if (article == null)
            throw new NotFoundException(nameof(Article), request.ArticleId);

        await _articleRepository.DeleteAsync(article);

        return Unit.Value;
    }
}
