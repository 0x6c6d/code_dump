using Application.Features.WarehouseManager.Groups.Repositories;

namespace Application.Features.WarehouseManager.Groups.Operations.Create;
public class CreateGroupValidator : AbstractValidator<CreateGroupRequest>
{
    private readonly IGroupRepository _groupRepository;

    public CreateGroupValidator(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;

        RuleFor(g => g.Name)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .NotNull()
            .MinimumLength(2).WithMessage("{PropertyName} muss mindestens 2 Zeichen lang sein.")
            .MaximumLength(100).WithMessage("{PropertyName} darf maximal 100 Zeichen lang sein.");

        RuleFor(a => a)
          .MustAsync(GroupNameUnique)
          .WithMessage("Eine Gruppe mit demselben Namen existiert bereits.");
    }

    private async Task<bool> GroupNameUnique(CreateGroupRequest e, CancellationToken token)
    {
        return !await _groupRepository.IsGroupNameUnique(e.Name);
    }
}
