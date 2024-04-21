namespace Application.Features.Procurements.Commands.Update;
public class UpdateProcurementCommandValidator : AbstractValidator<UpdateProcurementCommand>
{
    private readonly IProcurementRepository _procurementRepository;

    public UpdateProcurementCommandValidator(IProcurementRepository procurementRepository)
    {
        _procurementRepository = procurementRepository;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .NotNull()
            .MinimumLength(2).WithMessage("{PropertyName} muss mindestens 2 Zeichen lang sein.")
            .MaximumLength(100).WithMessage("{PropertyName} darf maximal 100 Zeichen lang sein.");

        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .NotNull()
            .EmailAddress().WithMessage("{PropertyName} ist keine gültige Email Adresse.");

        RuleFor(p => p.Phone)
            .NotEmpty()
                .When(u => !string.IsNullOrWhiteSpace(u.Phone)).WithMessage("{PropertyName} ist keine gültige Telefonnummer.")
            .Matches(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")
                .When(u => !string.IsNullOrWhiteSpace(u.Phone)).WithMessage("{PropertyName} ist keine gültige Telefonnummer.");

        RuleFor(a => a)
          .MustAsync(ProcurementNameUnique)
          .WithMessage("Ein Lieferant mit denselben Daten existiert bereits.");
    }

    private async Task<bool> ProcurementNameUnique(UpdateProcurementCommand e, CancellationToken token)
    {
        return !(await _procurementRepository.IsProcurementDataUnique(e.Name, e.Email, e.Phone, e.Link));
    }
}
