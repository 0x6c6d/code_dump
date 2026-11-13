using System.ComponentModel.DataAnnotations;

namespace Backend.Features.Command.Operations.Create;

public class CreateCommandRequest : IRequest<Guid>
{
    [MaxLength(45)]
    public required string Ip { get; set; }
    
    [MaxLength(1024)]
    public required string Command { get; set; }
    
    [MaxLength(2048)]
    public required string Response { get; set; }
}