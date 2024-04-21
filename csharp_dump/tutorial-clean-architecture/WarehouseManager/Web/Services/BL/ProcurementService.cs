namespace Web.Services.BL;

public class ProcurementService : IProcurementService
{
    private readonly IDataAccessProcurementService _dataAccessProcurementService;

    public ProcurementService(IDataAccessProcurementService dataAccessProcurementService)
    {
        _dataAccessProcurementService = dataAccessProcurementService;
    }

    public async Task<DataAccessResponse<ProcurementModel>> GetProcurement(Guid id)
    {
        if (id == Guid.Empty)
            throw new Exception("ProcurementId is empty");

        var result = await _dataAccessProcurementService.GetProcurementAsync(id);
        return result;
    }

    public async Task<DataAccessResponse<List<ProcurementModel>>> GetProcurements()
    {
        var result = await _dataAccessProcurementService.GetProcurementsAsync();
        return result;
    }

    public async Task<DataAccessResponse<Guid>> CreateProcurement(ProcurementModel procurement)
    {
        var result = await _dataAccessProcurementService.CreateProcurementAsync(procurement);
        return result;
    }

    public async Task<DataAccessResponse<bool>> UpdateProcurement(ProcurementModel procurement)
    {
        var result = await _dataAccessProcurementService.UpdateProcurementAsync(procurement);
        return result;
    }

    public async Task<DataAccessResponse<bool>> DeleteProcurement(Guid id)
    {
        var result = await _dataAccessProcurementService.DeleteProcurementAsync(id);
        return result;
    }
}