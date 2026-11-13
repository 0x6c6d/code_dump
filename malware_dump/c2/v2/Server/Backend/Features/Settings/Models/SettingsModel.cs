using System.ComponentModel.DataAnnotations;

namespace Backend.Features.Settings.Models;

public class SettingsModel : AuditableEntity
{
    public Guid Id { get; set; }
    
    [MaxLength(1024)]
    public required string DefaultCommand { get; set; }
    
    public required int Port { get; set; }
    
    public required int BufferSizeReceive { get; set; }
}