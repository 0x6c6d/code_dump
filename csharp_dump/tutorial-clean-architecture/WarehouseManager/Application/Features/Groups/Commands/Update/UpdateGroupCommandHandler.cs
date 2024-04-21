namespace Application.Features.Groups.Commands.Update;
public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, Unit>
{
    private readonly IGroupRepository _groupRepository;

    public UpdateGroupCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.GroupId);
        if (group == null)
            throw new NotFoundException(nameof(Group), request.GroupId);

        var validator = new UpdateGroupCommandValidator(_groupRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validationResult);

        Mappers.UpdateGroupCommandToGroup(request, group);
        group.LastModifiedDate = DateTime.Now;
        await _groupRepository.UpdateAsync(group);

        return Unit.Value;
    }
}
