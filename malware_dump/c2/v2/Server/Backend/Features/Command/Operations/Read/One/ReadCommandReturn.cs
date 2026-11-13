using System.ComponentModel.DataAnnotations;

namespace Backend.Features.Command.Operations.Read.One;

public class ReadCommandReturn: AuditableEntity
{
    public Guid Id { get; set; }

    [MaxLength(45)]
    public required string Ip { get; set; }
    
    [MaxLength(1024)]
    public required string Command { get; set; }
    
    [MaxLength(2048)]
    public required string Response { get; set; }
    
    public DateTime ExecutionDateTime { get; set; }
}