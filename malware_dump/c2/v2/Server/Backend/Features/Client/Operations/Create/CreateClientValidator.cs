using Backend.Features.Client.Repositories;

namespace Backend.Features.Client.Operations.Create;

public class CreateClientValidator : AbstractValidator<CreateClientRequest>
{
    private readonly IClientRepository _clientRepository;

    public CreateClientValidator(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
        
        RuleFor(a => a)
            .MustAsync(ClientIpUnique)
            .WithMessage("A database entry for a client with this ip already exists.");
    }
    
    private async Task<bool> ClientIpUnique(CreateClientRequest e, CancellationToken token)
    {
        return !await _clientRepository.IsClientIpUnique(e.Ip);
    }
}