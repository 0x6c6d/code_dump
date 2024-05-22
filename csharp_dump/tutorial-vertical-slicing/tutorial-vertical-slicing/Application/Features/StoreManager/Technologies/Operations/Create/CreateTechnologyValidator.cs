using Application.Features.StoreManager.Technologies.Repositories;

namespace Application.Features.StoreManager.Technologies.Operations.Create;
public class CreateTechnologyValidator : AbstractValidator<CreateTechnologyRequest>
{
    private readonly ITechnologyRepository _technologyRepository;

    public CreateTechnologyValidator(ITechnologyRepository technologyRepository)
    {
        _technologyRepository = technologyRepository;

        RuleFor(s => s.StoreId)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .NotNull()
            .Length(6, 6).WithMessage("{PropertyName} muss 6 Zeichen lang sein.");

        RuleFor(a => a)
          .MustAsync(StoreIdUnique)
          .WithMessage("Eine Filiale mit derselben Filialnummer existiert bereits.");
    }

    private async Task<bool> StoreIdUnique(CreateTechnologyRequest e, CancellationToken token)
    {
        return !await _technologyRepository.IsStoreIdUnique(e.StoreId);
    }
}
