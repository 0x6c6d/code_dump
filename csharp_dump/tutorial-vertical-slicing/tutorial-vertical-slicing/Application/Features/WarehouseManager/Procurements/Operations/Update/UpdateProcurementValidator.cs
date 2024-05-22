using Application.Features.WarehouseManager.Procurements.Repositories;

namespace Application.Features.WarehouseManager.Procurements.Operations.Update;
public class UpdateProcurementValidator : AbstractValidator<UpdateProcurementRequest>
{
    private readonly IProcurementRepository _procurementRepository;

    public UpdateProcurementValidator(IProcurementRepository procurementRepository)
    {
        _procurementRepository = procurementRepository;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .NotNull()
            .MinimumLength(2).WithMessage("{PropertyName} muss mindestens 2 Zeichen lang sein.")
            .MaximumLength(100).WithMessage("{PropertyName} darf maximal 100 Zeichen lang sein.");

        RuleFor(a => a)
          .MustAsync(ProcurementNameUnique)
          .WithMessage("Ein Lieferant mit denselben Namen existiert bereits.");
    }

    private async Task<bool> ProcurementNameUnique(UpdateProcurementRequest e, CancellationToken token)
    {
        var result = await _procurementRepository.GetByIdAsync(e.Id);
        if (e.Name.Equals(result?.Name, StringComparison.CurrentCultureIgnoreCase))
        {
            return true;
        }
        
        return !await _procurementRepository.IsProcurementNameUnique(e.Name);
    }
}
