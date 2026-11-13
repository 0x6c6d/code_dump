using Backend.Features.Client.Models;
using Backend.Features.Client.Repositories;
using ClientMapper = Backend.Features.Client.Models.ClientMapper;

namespace Backend.Features.Client.Operations.Create;

public class CreateClientHandler(IClientRepository clientRepository) : IRequestHandler<CreateClientRequest, Guid>
{
    public async Task<Guid> Handle(CreateClientRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateClientValidator(clientRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        var client = ClientMapper.CreateClientRequestToClient(request);
        client.LastConnection = DateTime.UtcNow;
        client.CreatedDateTime = DateTime.UtcNow;
        client.LastModifiedDateTime = DateTime.UtcNow;
        client = await clientRepository.AddAsync(client);

        return client.Id;
    }
}