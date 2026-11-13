using Backend.Common.Exceptions;
using Backend.Features.Client.Models;
using Backend.Features.Client.Repositories;
using ClientMapper = Backend.Features.Client.Models.ClientMapper;

namespace Backend.Features.Client.Operations.Update;

public class UpdateClientHandler(IClientRepository clientRepository) : IRequestHandler<UpdateClientRequest, Unit>
{
    public async Task<Unit> Handle(UpdateClientRequest request, CancellationToken cancellationToken)
    {
        var client = await clientRepository.GetByIdAsync(request.Id);
        if (client == null)
            throw new NotFoundException(nameof(ClientModel), request.Id);

        var validator = new UpdateClientValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        ClientMapper.UpdateClientRequestToClient(request, client);
        client.LastModifiedDateTime = DateTime.UtcNow;
        await clientRepository.UpdateAsync(client);

        return Unit.Value;
    }
}