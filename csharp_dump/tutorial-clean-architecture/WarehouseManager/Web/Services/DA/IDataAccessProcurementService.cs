
namespace Web.Services.DA;

public interface IDataAccessProcurementService
{
    Task<DataAccessResponse<Guid>> CreateProcurementAsync(ProcurementModel procurement);
    Task<DataAccessResponse<bool>> DeleteProcurementAsync(Guid id);
    Task<DataAccessResponse<ProcurementModel>> GetProcurementAsync(Guid id);
    Task<DataAccessResponse<List<ProcurementModel>>> GetProcurementsAsync();
    Task<DataAccessResponse<bool>> UpdateProcurementAsync(ProcurementModel procurement);
}