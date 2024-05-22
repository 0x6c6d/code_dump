using Application.Features.WarehouseManager.Procurements.Repositories;

namespace Application.Features.WarehouseManager.Procurements.Operations.Create;
public class CreateProcurementValidator : AbstractValidator<CreateProcurementRequest>
{
    private readonly IProcurementRepository _repository;

    public CreateProcurementValidator(IProcurementRepository repository)
    {
        _repository = repository;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .NotNull()
            .MinimumLength(2).WithMessage("{PropertyName} muss mindestens 2 Zeichen lang sein.")
            .MaximumLength(100).WithMessage("{PropertyName} darf maximal 100 Zeichen lang sein.");

        RuleFor(a => a)
          .MustAsync(ProcurementNameUnique)
          .WithMessage("Ein Lieferant mit demselben Namen existiert bereits.");
    }

    private async Task<bool> ProcurementNameUnique(CreateProcurementRequest e, CancellationToken token)
    {
        return !await _repository.IsProcurementNameUnique(e.Name);
    }
}
