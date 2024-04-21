namespace Application.Features.Articles.Commands.Update;
public class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
{
    private readonly IArticleRepository _articleRepository;

    public UpdateArticleCommandValidator(IArticleRepository article)
    {
        _articleRepository = article;

        RuleFor(a => a.Name)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .NotNull()
            .MinimumLength(2).WithMessage("{PropertyName} muss mindestens 2 Zeichen lang sein.")
            .MaximumLength(50).WithMessage("{PropertyName} darf 50 Zeichen nicht überschreiten.");

        RuleFor(a => a.ItemNumber)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .NotNull()
            .MinimumLength(2).WithMessage("{PropertyName} muss mindestens 2 Zeichen lang sein.")
            .MaximumLength(50).WithMessage("{PropertyName} darf 50 Zeichen nicht überschreiten.");

        RuleFor(a => a.Stock)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .GreaterThan(0).WithMessage("{PropertyName} muss größer als null sein.");

        RuleFor(a => a.MinStock)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .GreaterThan(0).WithMessage("{PropertyName} muss größer als null sein.");
    }
}
