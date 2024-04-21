
namespace Web.Services.BL;

public interface IProcurementService
{
    Task<DataAccessResponse<Guid>> CreateProcurement(ProcurementModel procurement);
    Task<DataAccessResponse<bool>> DeleteProcurement(Guid id);
    Task<DataAccessResponse<ProcurementModel>> GetProcurement(Guid id);
    Task<DataAccessResponse<List<ProcurementModel>>> GetProcurements();
    Task<DataAccessResponse<bool>> UpdateProcurement(ProcurementModel procurement);
}