using Application.Features.Common.AX;
using Application.Features.StoreManager.Technologies;
using Application.Features.StoreManager.Technologies.Models;
using Presentation.Components.Pages.MasterKennung.Shared;

namespace Presentation.Components.Pages.MasterKennung;

public partial class Main
{
    private Header headerRef = new();
    private string storeId = string.Empty;
    private StoreInformation storeInformation = new();
    private Technology technology = new();
    private TechnologyVm technologyVm = new();

    private async Task HandleStoreChange(string newStoreId)
    {
        headerRef.DisplayMessage(string.Empty, false);

        if (string.IsNullOrEmpty(newStoreId) || newStoreId.Length != 6)
        {
            storeInformation = new();
            technologyVm = new();
            return;
        }

        // read store infos from ax
        storeId = newStoreId;
        var resultStoreInfo = await axService.GetStoreInformationAsync(storeId);
        if (resultStoreInfo == null || !resultStoreInfo.Success || resultStoreInfo.Data == null)
        {
            var message = string.IsNullOrEmpty(resultStoreInfo?.Message) ? "Es ist ein Fehler beim Holen der Filialinfos aufgetreten." : resultStoreInfo.Message;
            headerRef.DisplayMessage(message, true);
            return;
        }

        storeInformation = resultStoreInfo.Data;

        // get store entity
        var resultStore = await storeService.GetStoreAsync(storeId);
        if (resultStore == null || !resultStore.Success || resultStore.Data == null)
        {
            var message = string.IsNullOrEmpty(resultStore?.Message) ? "Es ist ein Fehler beim Holen der Filiale aufgetreten." : resultStore.Message;
            headerRef.DisplayMessage(message, true);
            return;
        }

        // add description to Store table
        if (string.IsNullOrEmpty(resultStore.Data.Description) && !string.IsNullOrEmpty(storeInformation.Description))
        {
            await storeService.UpdateStoreAsync(storeId, storeInformation.Description);
        }

        StateHasChanged();

        // read store technology from database
        var resultTechnology = await technologyService.GetTechnologyAsync(storeId);
        if (resultTechnology == null || !resultTechnology.Success || resultTechnology.Data == null)
        {
            var message = string.IsNullOrEmpty(resultTechnology?.Message) ? "Es ist ein Fehler beim Holen der Filialtechnik aufgetreten." : resultTechnology.Message;
            headerRef.DisplayMessage(message, true);
            return;
        }

        technology = resultTechnology.Data;
        technologyVm = TechnologyMapper.TechnologyToTechnologyVm(resultTechnology.Data);
        StateHasChanged();
    }

    private async Task SaveStoreData()
    {
        if (!ModelIsValid())
        {
            return;
        }

        var technology = TechnologyMapper.TechnologyVmToTechnology(technologyVm);
        var result = await technologyService.UpdateTechnologyAsync(technology);

        if (result == null || !result.Success || result.Data == false)
        {
            var message = string.IsNullOrEmpty(result?.Message) ? "Es ist ein Fehler beim Updaten der Filialtechnik aufgetreten." : result.Message;
            headerRef.DisplayMessage(message, true);
            return;
        }
        else
        {
            var message = "Daten erfolgreich geupdated";
            headerRef.DisplayMessage(message, false);
            headerRef.ActivateStoreSelection(true);
            headerRef.DisplayMessage(string.Empty, false);
        }
    }

    private async Task RevertStoreData()
    {
        var result = await technologyService.GetTechnologyAsync(storeId);
        if (result == null || !result.Success || result.Data == null)
        {
            var message = string.IsNullOrEmpty(result?.Message) ? "Es ist ein Fehler beim Holen der Filialtechnik aufgetreten." : result.Message;
            headerRef.DisplayMessage(message, true);
            return;
        }

        technologyVm = TechnologyMapper.TechnologyToTechnologyVm(result.Data);
        headerRef.ActivateStoreSelection(true);
        headerRef.DisplayMessage(string.Empty, false);
    }

    #region Helper
    private bool ModelIsValid()
    {
        if (string.IsNullOrEmpty(technologyVm.InternetConnectionId) && !string.IsNullOrEmpty(technologyVm.InternetAccessId))
        {
            headerRef.DisplayMessage("Bitte füllen Sie Anschlusskennung und Zugangsnummer aus.", true);
            return false;
        }

        if (!string.IsNullOrEmpty(technologyVm.InternetConnectionId) && string.IsNullOrEmpty(technologyVm.InternetAccessId))
        {
            headerRef.DisplayMessage("Bitte füllen Sie Anschlusskennung und Zugangsnummer aus.", true);
            return false;
        }

        if (!string.IsNullOrEmpty(technologyVm.InternetConnectionId) && !string.IsNullOrEmpty(technologyVm.InternetAccessId))
        {
            technologyVm.InternetUserName = $"{technologyVm.InternetConnectionId}{technologyVm.InternetAccessId}0001@t-online.de";
        }

        return true;
    }

    private void HandleInternetDataChanges()
    {
        if (string.IsNullOrEmpty(technologyVm.InternetConnectionId) || string.IsNullOrEmpty(technologyVm.InternetAccessId))
        {
            technologyVm.InternetUserName = string.Empty;
            return;
        }

        if (ModelIsValid())
        {
            technologyVm.InternetUserName = $"{technologyVm.InternetConnectionId}{technologyVm.InternetAccessId}0001@t-online.de";
        }
    }

    private void HandleInputChanges()
    {
        if (technologyVm.Equals(technology))
        {
            headerRef.ActivateStoreSelection(true);
            headerRef.DisplayMessage(string.Empty, false);
        }
        else
        {
            headerRef.ActivateStoreSelection(false);
            headerRef.DisplayMessage("Änderungen speichern oder verwerfen, bevor eine neue Filiale ausgewählt werden kann", false);
        }
    }
    #endregion
}
