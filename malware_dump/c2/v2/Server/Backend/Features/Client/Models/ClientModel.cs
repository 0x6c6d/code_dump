using System.ComponentModel.DataAnnotations;
using Backend.Common.Models;

namespace Backend.Features.Client.Models;

public class ClientModel : AuditableEntity
{
    public Guid Id { get; set; }
    
    [MaxLength(45)]
    public required string Ip { get; set; }

    public required int Port { get; set; }
    
    [MaxLength(1024)]
    public required string Command { get; set; }
    
    public DateTime LastConnection { get; set; }
}