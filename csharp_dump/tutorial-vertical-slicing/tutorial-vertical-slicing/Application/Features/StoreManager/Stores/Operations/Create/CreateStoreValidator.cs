using Application.Features.StoreManager.Stores.Repositories;

namespace Application.Features.StoreManager.Stores.Operations.Create;
public class CreateStoreValidator : AbstractValidator<CreateStoreRequest>
{
    private readonly IStoreRepository _storeRepository;

    public CreateStoreValidator(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;

        RuleFor(s => s.StoreId)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .NotNull()
            .Length(6, 6).WithMessage("{PropertyName} muss 6 Zeichen lang sein.");

        RuleFor(a => a)
          .MustAsync(StoreIdUnique)
          .WithMessage("Eine Filiale mit derselben Filialnummer existiert bereits.");
    }

    private async Task<bool> StoreIdUnique(CreateStoreRequest e, CancellationToken token)
    {
        return !await _storeRepository.IsStoreIdUnique(e.StoreId);
    }
}
