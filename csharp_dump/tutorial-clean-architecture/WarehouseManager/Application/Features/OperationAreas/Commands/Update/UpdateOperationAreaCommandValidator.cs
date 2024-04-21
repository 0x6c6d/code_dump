namespace Application.Features.OperationAreas.Commands.Update;
public class UpdateOperationAreaCommandValidator : AbstractValidator<UpdateOperationAreaCommand>
{
    private readonly IOperationAreaRepository _operationAreaRepository;

    public UpdateOperationAreaCommandValidator(IOperationAreaRepository operationAreaRepository)
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

    private async Task<bool> OperationAreaNameUnique(UpdateOperationAreaCommand e, CancellationToken token)
    {
        return !(await _operationAreaRepository.IsOperationAreaNameUnique(e.Name));
    }
}
