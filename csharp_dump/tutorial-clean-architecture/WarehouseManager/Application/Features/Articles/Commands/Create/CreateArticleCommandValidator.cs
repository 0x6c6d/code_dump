namespace Application.Features.Articles.Commands.Create;
public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    private readonly IArticleRepository _articleRepository;

    public CreateArticleCommandValidator(IArticleRepository article)
    {
        _articleRepository = article;

        RuleFor(a => a.Name)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .NotNull()
            .MinimumLength(2).WithMessage("{PropertyName} muss mindestens 2 Zeichen lang sein.")
            .MaximumLength(50).WithMessage("{PropertyName} darf maximal 50 Zeichen lang sein.");

        RuleFor(a => a.ItemNumber)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .NotNull()
            .MinimumLength(2).WithMessage("{PropertyName} muss mindestens 2 Zeichen lang sein.")
            .MaximumLength(50).WithMessage("{PropertyName} darf maximal 50 Zeichen lang sein.");

        RuleFor(a => a.Stock)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .GreaterThan(0).WithMessage("{PropertyName} muss größer als null sein.");

        RuleFor(a => a.MinStock)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .GreaterThan(0).WithMessage("{PropertyName} muss größer als null sein.");

        RuleFor(a => a)
           .MustAsync(ArticleNameAndItemNumberUnique)
           .WithMessage("Ein Artikel mit demselben Namen und Artikelnummer existiert bereits.");
    }

    private async Task<bool> ArticleNameAndItemNumberUnique(CreateArticleCommand e, CancellationToken token)
    {
        return !(await _articleRepository.IsArticleNameAndItemNumberUnique(e.Name, e.ItemNumber));
    }
}
