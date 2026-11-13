using Backend.Common.Models;

namespace Backend.Features.Client.Operations.Read.One;

public class ReadClientReturn : AuditableEntity
{
    public Guid Id { get; init; }
    public string Ip { get; init; } = string.Empty;
    public int Port { get; init; }
    public string Command { get; set; } = string.Empty;
}