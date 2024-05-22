using Application.Features.StoreManager.Events.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StoreManager.Events.Respositories;
public class EventRepository : BaseRepository<Event>, IEventRepository
{
    public EventRepository(DataContext dataContext) : base(dataContext) { }

    public async Task<List<Event>> GetAllByStoreIdAsync(string storeId)
    {
        var events = await _dbContext.Events.Where(s => s.StoreId == storeId).ToListAsync();
        return events;
    }

    public async Task<bool> IsEventUnique(int eventId, DateOnly date, TimeOnly time)
    {
        var foundEvent = await _dbContext.Events.AnyAsync(s => s.EventId == eventId && s.Date == date && s.Time == time);
        return foundEvent;
    }
}
