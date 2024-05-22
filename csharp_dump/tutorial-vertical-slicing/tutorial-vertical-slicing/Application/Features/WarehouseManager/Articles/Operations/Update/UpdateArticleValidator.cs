namespace Application.Features.WarehouseManager.Articles.Operations.Update;
public class UpdateArticleValidator : AbstractValidator<UpdateArticleRequest>
{
    public UpdateArticleValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .NotNull()
            .MinimumLength(2).WithMessage("{PropertyName} muss mindestens 2 Zeichen lang sein.")
            .MaximumLength(50).WithMessage("{PropertyName} darf 50 Zeichen nicht überschreiten.");

        RuleFor(a => a.MinStock)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .GreaterThan(0).WithMessage("{PropertyName} muss größer als null sein.");
    }
}
