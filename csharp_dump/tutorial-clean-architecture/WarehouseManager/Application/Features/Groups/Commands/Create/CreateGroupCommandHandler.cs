namespace Application.Features.Groups.Commands.Create;
public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Guid>
{
    private readonly IGroupRepository _groupRepository;

    public CreateGroupCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<Guid> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateGroupCommandValidator(_groupRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validationResult);

        var group = Mappers.CreateGroupCommandToGroup(request);
        group.CreatedDate = DateTime.Now;
        group.LastModifiedDate = DateTime.Now;
        group = await _groupRepository.AddAsync(group);

        return group.GroupId;
    }
}
