using System.ComponentModel.DataAnnotations;

namespace Application.Features.StoreManager.Events.Models;
public class EventType
{
    [Key]
    public int Id { get; set; }

    public string EventName { get; set; } = string.Empty;
}
