using Backend.Features.Command.Models;
using Backend.Features.Command.Repositories;

namespace Backend.Features.Command.Operations.Read.One;

public class ReadCommandHandler(ICommandRepository commandRepository): IRequestHandler<ReadCommandRequest, ReadCommandReturn>
{
    public async Task<ReadCommandReturn> Handle(ReadCommandRequest request, CancellationToken cancellationToken)
    {
        var client = await commandRepository.GetByIdAsync(request.Id);
        if (client == null)
        {
            throw new NotFoundException(nameof(CommandModel), request.Id);
        }

        var getClientReturn = CommandMapper.CommandToGetCommandReturn(client);
        return getClientReturn;
    }
}