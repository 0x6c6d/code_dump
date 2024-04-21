namespace Application.Features.Groups.Commands.Create;
public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    private readonly IGroupRepository _groupRepository;

    public CreateGroupCommandValidator(IGroupRepository groupRepository)
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

    private async Task<bool> GroupNameUnique(CreateGroupCommand e, CancellationToken token)
    {
        return !(await _groupRepository.IsGroupNameUnique(e.Name));
    }
}
