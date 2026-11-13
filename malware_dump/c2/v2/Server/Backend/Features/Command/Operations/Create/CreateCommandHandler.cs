using Backend.Features.Command.Models;
using Backend.Features.Command.Repositories;

namespace Backend.Features.Command.Operations.Create;

public class CreateCommandHandler(ICommandRepository commandRepository) : IRequestHandler<CreateCommandRequest, Guid>
{
    public async Task<Guid> Handle(CreateCommandRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        var command = CommandMapper.CreateCommandRequestToCommand(request);
        command.ExecutionDateTime = DateTime.UtcNow;
        command.CreatedDateTime = DateTime.UtcNow;
        command.LastModifiedDateTime = DateTime.UtcNow;
        command = await commandRepository.AddAsync(command);

        return command.Id;
    }
}