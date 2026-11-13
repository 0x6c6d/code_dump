using Backend.Common.Exceptions;
using Backend.Features.Client.Repositories;

namespace Backend.Features.Client.Operations.Delete;

public class DeleteClientHandler(IClientRepository clientRepository) : IRequestHandler<DeleteClientRequest, Unit>
{
    public async Task<Unit> Handle(DeleteClientRequest request, CancellationToken cancellationToken)
    {
        var client = await clientRepository.GetByIdAsync(request.Id);
        if (client == null)
        {
            throw new NotFoundException(nameof(client), request.Id);
        }

        await clientRepository.DeleteAsync(client);
        return Unit.Value;
    }
}