using System.ComponentModel.DataAnnotations;
using Backend.Common.Models;

namespace Backend.Features.Command.Operations.Read.PerClient;

public class ReadCommandsByIpReturn : AuditableEntity
{
    public Guid Id { get; set; }

    [MaxLength(45)] public required string Ip { get; set; }

    [MaxLength(1024)] public required string Command { get; set; }

    [MaxLength(2048)] public required string Response { get; set; }

    public DateTime ExecutionDateTime { get; set; }
}