using Backend.Common.Exceptions;
using Backend.Features.Command.Repositories;

namespace Backend.Features.Command.Operations.Delete.ByIp;

public class DeleteCommandsByIpHandler(ICommandRepository commandRepository) : IRequestHandler<DeleteCommandsByIpRequest, Unit>
{
    public async Task<Unit> Handle(DeleteCommandsByIpRequest request, CancellationToken cancellationToken)
    {
        var commands = await commandRepository.GetCommandsByIp(request.Ip);
        if (commands == null)
        {
            throw new NotFoundException(nameof(commands), request.Ip);
        }

        foreach (var command in commands)
        {
            await commandRepository.DeleteAsync(command);
        }
        
        return Unit.Value;
    }
}