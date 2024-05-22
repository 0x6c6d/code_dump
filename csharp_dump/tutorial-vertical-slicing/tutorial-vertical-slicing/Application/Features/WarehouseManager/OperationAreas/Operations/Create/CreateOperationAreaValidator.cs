using Application.Features.WarehouseManager.OperationAreas.Repositories;

namespace Application.Features.WarehouseManager.OperationAreas.Operations.Create;
public class CreateOperationAreaValidator : AbstractValidator<CreateOperationAreaRequest>
{
    private readonly IOperationAreaRepository _operationAreaRepository;

    public CreateOperationAreaValidator(IOperationAreaRepository operationAreaRepository)
    {
        _operationAreaRepository = operationAreaRepository;

        RuleFor(oa => oa.Name)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .NotNull()
            .MinimumLength(2).WithMessage("{PropertyName} muss mindestens 2 Zeichen lang sein.")
            .MaximumLength(100).WithMessage("{PropertyName} darf maximal 100 Zeichen lang sein.");

        RuleFor(a => a)
          .MustAsync(OperationAreaNameUnique)
          .WithMessage("Ein Einsatzgebiet mit demselben Namen existiert bereits.");
    }

    private async Task<bool> OperationAreaNameUnique(CreateOperationAreaRequest e, CancellationToken token)
    {
        return !await _operationAreaRepository.IsOperationAreaNameUnique(e.Name);
    }
}
