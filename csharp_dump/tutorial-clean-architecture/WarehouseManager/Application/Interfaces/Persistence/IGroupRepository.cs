namespace Application.Contracts.Persistence;
public interface IGroupRepository : IRepository<Group>
{
    Task<bool> IsGroupNameUnique(string name);
}
