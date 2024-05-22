using Application.Features.StoreManager.Events.Models;

namespace Application.Features.StoreManager.Events.Respositories;
public class EventTypeRepository : BaseRepository<EventType>, IEventTypeRepository
{
    public EventTypeRepository(DataContext dataContext) : base(dataContext) { }
}
