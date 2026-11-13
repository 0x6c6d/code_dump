using Backend.Common.Exceptions;
using Backend.Features.Command.Models;
using Backend.Features.Command.Repositories;
using Backend.Features.Client.Models;

namespace Backend.Features.Command.Operations.Update;

public class UpdateCommandHandler(ICommandRepository commandRepository) : IRequestHandler<UpdateCommandRequest, Unit>
{
    public async Task<Unit> Handle(UpdateCommandRequest request, CancellationToken cancellationToken)
    {
        var command = await commandRepository.GetByIdAsync(request.Id);
        if (command == null)
            throw new NotFoundException(nameof(CommandModel), request.Id);

        var validator = new UpdateClientValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        CommandMapper.UpdateCommandRequestToCommand(request, command);
        command.LastModifiedDateTime = DateTime.UtcNow;
        await commandRepository.UpdateAsync(command);

        return Unit.Value;
    }
}