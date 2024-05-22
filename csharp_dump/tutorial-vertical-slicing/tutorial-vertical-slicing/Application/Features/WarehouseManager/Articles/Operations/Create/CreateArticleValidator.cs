namespace Application.Features.WarehouseManager.Articles.Operations.Create;
public class CreateArticleValidator : AbstractValidator<CreateArticleRequest>
{
    public CreateArticleValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .NotNull()
            .MinimumLength(2).WithMessage("{PropertyName} muss mindestens 2 Zeichen lang sein.")
            .MaximumLength(50).WithMessage("{PropertyName} darf maximal 50 Zeichen lang sein.");

        RuleFor(a => a.MinStock)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .GreaterThan(0).WithMessage("{PropertyName} muss größer als null sein.");
    }
}