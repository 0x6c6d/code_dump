using Application.Features.StoreManager.Stores;
using Application.Features.StoreManager.Stores.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Presentation.Components.Pages.MasterKennung.Shared;

public partial class Header
{
    [Parameter] public EventCallback<string> StoreChange { get; set; }
    [Parameter] public EventCallback Save { get; set; }
    [Parameter] public EventCallback Revert { get; set; }

    private AddStore addStore = new();
    private DeleteStore deleteStore = new();
    private Events storeEvents = new();

    private bool storeSelectionEnabled = true;
    private bool searchInputEnabled = true;
    private bool showRemoveIcon = false;
    private string searchInput = string.Empty;
    private bool messageError = false;
    private string message = string.Empty;
    private List<Store> storesFiltered = new();
    private List<Store> storesAll = new();
    private string selectedStoreId = string.Empty;
    private string storeInputField = string.Empty;


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadData();
        }
    }

    private async Task HandleStoreSelection(ChangeEventArgs? e)
    {
        messageError = false;
        message = string.Empty;
        showRemoveIcon = false;

        var storeId = e?.Value?.ToString() ?? string.Empty;
        if (e == null || string.IsNullOrEmpty(storeId))
        {
            selectedStoreId = string.Empty;
            ChangeSearchInputActivationStatus(true);
            await StoreChange.InvokeAsync((string.Empty));
            return;
        }

        if (storesFiltered.Count() == 0 || storeId.Length != 6)
        {
            ChangeSearchInputActivationStatus(false);
            return;
        }

        if (storesFiltered.Any(s => s.StoreId == storeId))
        {
            selectedStoreId = storeId;
            ChangeSearchInputActivationStatus(false);
            var store = storesFiltered.FirstOrDefault(s => s.StoreId == storeId);
            await StoreChange.InvokeAsync((storeId));
        }

        showRemoveIcon = true;
    }

    private async Task HandleDelete()
    {
        await LoadData();
        await jsRuntime.InvokeVoidAsync("clearStoreInputField");
        await HandleStoreSelection(null);

        StateHasChanged();
    }

    private async Task HandleAdd(string storeId)
    {
        var result = await storeService.GetStoreAsync(storeId);
        if (result == null || !result.Success || result.Data == null)
        {
            message = string.IsNullOrEmpty(result?.Message) ? "Es ist ein Fehler beim Holen der Filiale aufgetreten." : result.Message;
            StateHasChanged();
            return;
        }

        storesFiltered.Add(result.Data);
        storesFiltered = storesFiltered.OrderBy(x => x.StoreId).ToList();

        await jsRuntime.InvokeVoidAsync("addStoreInputField", storeId);
        await HandleStoreSelection(new() { Value = storeId });
    }

    #region Button
    private async Task SaveStoreData()
    {
        if (string.IsNullOrEmpty(selectedStoreId))
        {
            messageError = true;
            message = "Bitte Filiale auswählen";
            return;
        }

        await Save.InvokeAsync();
    }

    private async Task RevertData()
    {
        if (string.IsNullOrEmpty(selectedStoreId))
        {
            messageError = true;
            message = "Bitte Filiale auswählen";
            return;
        }

        await Revert.InvokeAsync();
    }

    private void OpenAddStore()
    {
        addStore.OpenModal();
    }

    private void OpenEvents()
    {
        if (string.IsNullOrEmpty(selectedStoreId))
        {
            messageError = true;
            message = "Bitte Filiale auswählen";
            return;
        }

        storeEvents.OpenModal(selectedStoreId);
    }

    private void DeleteStore()
    {
        if (string.IsNullOrEmpty(selectedStoreId))
        {
            messageError = true;
            message = "Bitte Filiale auswählen";
            return;
        }

        deleteStore.OpenModal(selectedStoreId);
    }

    private async Task RemoveStoreSelection()
    {
        storeInputField = string.Empty;
        await HandleStoreSelection(null);

    }
    #endregion

    #region Search Input
    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            await QuerryStoreInformation();
        }
    }

    private void UpdateSearchInput(ChangeEventArgs e)
    {
        searchInput = e?.Value?.ToString() ?? string.Empty;
    }

    private async Task QuerryStoreInformation()
    {
        messageError = false;
        message = string.Empty;
        selectedStoreId = string.Empty;
        storesFiltered = storesAll;

        if (string.IsNullOrEmpty(searchInput))
        {
            messageError = false;
            message = string.Empty;
            return;
        }

        if (searchInput.Length < 5)
        {
            messageError = true;
            message = "Minimallänge für eine Suche beträgt 5 Zeichen";
            return;
        }

        var result = await technologyService.SearchTechnologyForString(searchInput);
        if (result == null)
        {
            messageError = true;
            message = string.IsNullOrEmpty(result?.Message) ? "Fehler bei der Suche aufgetreten" : result.Message;
            return;
        }
        else if (result.Data == null || result.Data.Count == 0)
        {
            messageError = false;
            message = $"Es wurden keine Suchergebnisse für '{searchInput}' gefunden";
            return;
        }
        else
        {
            messageError = false;
            message = "Filialen die auf die Suche zutreffen können nun ausgewählt werden";
        }

        storesFiltered = storesFiltered.Where(x => result.Data.Contains(x.StoreId)).ToList();
        if (storesFiltered.Count == 1)
        {
            showRemoveIcon = true;
            searchInputEnabled = false;
            selectedStoreId = storesFiltered[0].StoreId;
            storeInputField = selectedStoreId;
            await StoreChange.InvokeAsync(selectedStoreId);
        }

        StateHasChanged();
    }
    #endregion

    #region Helper
    public void DisplayMessage(string msg, bool error)
    {
        messageError = error;
        message = msg;
    }

    public void ActivateStoreSelection(bool status)
    {
        storeSelectionEnabled = status;
        StateHasChanged();
    }

    private void ChangeSearchInputActivationStatus(bool status)
    {
        searchInputEnabled = status;
    }

    private async Task LoadData()
    {
        var result = await storeService.GetStoresAsync();
        if (result == null || !result.Success)
        {
            message = string.IsNullOrEmpty(result?.Message) ? "Es ist ein Fehler beim Holen der Filiale aufgetreten." : result.Message;
            StateHasChanged();
            return;
        }

        if (result.Data == null || result.Data.Count == 0)
        {
            message = "Es wurden keine Filialen gefunden";
            StateHasChanged();
            return;
        }

        storesFiltered = result.Data;
        storesAll = StoreMapperDeepClone.StoresDeepClone(storesFiltered);
        StateHasChanged();
    }
    #endregion
}
