namespace Presentation.Components.Pages.WarehouseManager;

public partial class Settings
{
    private bool isGroupActive = true;
    private string groupColor => isGroupActive ? "#0D202F" : "#9A9A9A";
    private string groupUnderscore => isGroupActive ? "underline;" : "";

    private bool isCategoryActive = false;
    private string categoryColor => isCategoryActive ? "#0D202F" : "#9A9A9A";
    private string categoryUnderscore => isCategoryActive ? "underline;" : "";

    private bool isStoragePlaceActive = false;
    private string storagePlaceColor => isStoragePlaceActive ? "#0D202F" : "#9A9A9A";
    private string storagePlaceUnderscore => isStoragePlaceActive ? "underline;" : "";

    private bool isProcurementActive = false;
    private string procurementColor => isProcurementActive ? "#0D202F" : "#9A9A9A";
    private string procurementUnderscore => isProcurementActive ? "underline;" : "";

    private bool isOperationAreaActive = false;
    private string operationAreaColor => isOperationAreaActive ? "#0D202F" : "#9A9A9A";
    private string operationAreaUnderscore => isOperationAreaActive ? "underline;" : "";

    private void ActivateGroup()
    {
        isGroupActive = true;
        isCategoryActive = false;
        isStoragePlaceActive = false;
        isProcurementActive = false;
        isOperationAreaActive = false;
    }

    private void ActivateCategory()
    {
        isGroupActive = false;
        isCategoryActive = true;
        isStoragePlaceActive = false;
        isProcurementActive = false;
        isOperationAreaActive = false;
    }

    private void ActivateStoragePlace()
    {
        isGroupActive = false;
        isCategoryActive = false;
        isStoragePlaceActive = true;
        isProcurementActive = false;
        isOperationAreaActive = false;
    }

    private void ActivateProcurement()
    {
        isGroupActive = false;
        isCategoryActive = false;
        isStoragePlaceActive = false;
        isProcurementActive = true;
        isOperationAreaActive = false;
    }

    private void ActivateOperationArea()
    {
        isGroupActive = false;
        isCategoryActive = false;
        isStoragePlaceActive = false;
        isProcurementActive = false;
        isOperationAreaActive = true;
    }
}
