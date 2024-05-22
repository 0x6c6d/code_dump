using Application.Features.WarehouseManager.StoragePlaces.Repositories;

namespace Application.Features.WarehouseManager.StoragePlaces.Operations.Create;
public class CreateStoragePlaceValidator : AbstractValidator<CreateStoragePlaceRequest>
{
    private readonly IStoragePlaceRepository _storagePlaceRepository;

    public CreateStoragePlaceValidator(IStoragePlaceRepository storagePlaceRepository)
    {
        _storagePlaceRepository = storagePlaceRepository;

        RuleFor(sp => sp.Name)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .NotNull()
            .MinimumLength(2).WithMessage("{PropertyName} muss mindestens 2 Zeichen lang sein.")
            .MaximumLength(100).WithMessage("{PropertyName} darf maximal 100 Zeichen lang sein.");

        RuleFor(a => a)
          .MustAsync(StoragePlaceNameUnique)
          .WithMessage("Ein Lagerplatz mit demselben Namen existiert bereits.");
    }

    private async Task<bool> StoragePlaceNameUnique(CreateStoragePlaceRequest e, CancellationToken token)
    {
        return !(await _storagePlaceRepository.IsStoragePlaceNameUnique(e.Name));
    }
}
