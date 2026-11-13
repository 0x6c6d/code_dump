using Backend.Common.Exceptions;
using Backend.Features.Command.Repositories;

namespace Backend.Features.Command.Operations.Delete.ById;

public class DeleteCommandByIdHandler(ICommandRepository commandRepository) : IRequestHandler<DeleteCommandByIdRequest, Unit>
{
    public async Task<Unit> Handle(DeleteCommandByIdRequest request, CancellationToken cancellationToken)
    {
        var command = await commandRepository.GetByIdAsync(request.Id);
        if (command == null)
        {
            throw new NotFoundException(nameof(command), request.Id);
        }

        await commandRepository.DeleteAsync(command);
        return Unit.Value;
    }
}