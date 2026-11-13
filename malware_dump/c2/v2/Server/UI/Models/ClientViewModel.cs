using System.ComponentModel.DataAnnotations;

namespace UI.Models;

public class ClientViewModel
{
    public Guid Id { get; set; }
    
    [MaxLength(45)]
    public string? Ip { get; set; }

    public int Port { get; set; }
    
    [MaxLength(512)]
    public string? Command { get; set; }
    
    public DateTime LastConnection { get; set; }
    
    public DateTime CreatedDateTime { get; set; }
    
    public DateTime LastModifiedDateTime { get; set; }

    public bool Checked { get; set; }
}