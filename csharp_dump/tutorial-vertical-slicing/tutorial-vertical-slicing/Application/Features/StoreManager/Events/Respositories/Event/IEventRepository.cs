using Application.Features.StoreManager.Events.Models;

namespace Application.Features.StoreManager.Events.Respositories;
public interface IEventRepository : IRepository<Event>
{
    Task<List<Event>> GetAllByStoreIdAsync(string storeId);
    Task<bool> IsEventUnique(int eventId, DateOnly date, TimeOnly time);
}
