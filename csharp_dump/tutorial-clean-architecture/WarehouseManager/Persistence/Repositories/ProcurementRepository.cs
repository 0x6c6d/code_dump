namespace Persistence.Repositories;
public class ProcurementRepository : BaseRepository<Procurement>, IProcurementRepository
{
    public ProcurementRepository(DataContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> IsProcurementDataUnique(string name, string email, string phone, string link)
    {
        var match = _dbContext.Procurements.Any(a =>
            a.Name.ToLower() == name.ToLower() &&
            a.Email.ToLower() == email.ToLower() &&
            a.Phone.ToLower() == phone.ToLower() &&
            a.Link.ToLower() == link.ToLower());
        return Task.FromResult(match);
    }
}
