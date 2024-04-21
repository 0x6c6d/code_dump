namespace Application.Contracts.Persistence;
public interface IOperationAreaRepository : IRepository<OperationArea>
{
    Task<bool> IsOperationAreaNameUnique(string name);
}
