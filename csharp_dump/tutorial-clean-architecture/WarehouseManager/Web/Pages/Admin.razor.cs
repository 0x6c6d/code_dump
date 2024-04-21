namespace Web.Pages;

public partial class Admin
{
    private bool IsAccountActive = true;
    private string GroupColor => IsAccountActive ? "#0D202F" : "#000";
    private string GroupUnderscore => IsAccountActive ? "underline;" : "";

    private bool IsStoragePlaceActive = false;
    private string StoragePlaceColor => IsStoragePlaceActive ? "#0D202F" : "#000";
    private string StoragePlaceUnderscore => IsStoragePlaceActive ? "underline;" : "";

    private bool IsProcurementActive = false;
    private string ProcurementColor => IsProcurementActive ? "#0D202F" : "#000";
    private string ProcurementUnderscore => IsProcurementActive ? "underline;" : "";

    private bool IsOperationAreaActive = false;
    private string OperationAreaColor => IsOperationAreaActive ? "#0D202F" : "#000";
    private string OperationAreaUnderscore => IsOperationAreaActive ? "underline;" : "";

    private void ActivateGroup()
    {
        IsAccountActive = true;
        IsStoragePlaceActive = false;
        IsProcurementActive = false;
        IsOperationAreaActive = false;
    }

    private void ActivateStoragePlace()
    {
        IsAccountActive = false;
        IsStoragePlaceActive = true;
        IsProcurementActive = false;
        IsOperationAreaActive = false;
    }

    private void ActivateProcurement()
    {
        IsAccountActive = false;
        IsStoragePlaceActive = false;
        IsProcurementActive = true;
        IsOperationAreaActive = false;
    }

    private void ActivateOperationArea()
    {
        IsAccountActive = false;
        IsStoragePlaceActive = false;
        IsProcurementActive = false;
        IsOperationAreaActive = true;
    }
}
