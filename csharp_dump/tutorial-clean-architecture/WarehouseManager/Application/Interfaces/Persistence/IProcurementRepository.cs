namespace Application.Contracts.Persistence;
public interface IProcurementRepository : IRepository<Procurement>
{
    Task<bool> IsProcurementDataUnique(string name, string email, string phone, string link);
}
