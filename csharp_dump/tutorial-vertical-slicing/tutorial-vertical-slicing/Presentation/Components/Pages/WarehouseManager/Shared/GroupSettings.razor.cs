using Application.Features.WarehouseManager.Groups.Models;

namespace Presentation.Components.Pages.WarehouseManager.Shared;

public partial class GroupSettings
{
    public string messageTop = string.Empty;
    public string messageBottom = string.Empty;
    public bool messageError = false;
    public Group groupNew = new();
    public Group groupSelected = new();
    public List<Group> groups = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await GetGroups();
            StateHasChanged();
        }
    }

    private void HandleChangeEvent(Guid id)
    {
        if (id == Guid.Empty)
        {
            groupSelected = new();
        }
        else
        {
            groupSelected.Id = id;
            groupSelected.Name = groups.FirstOrDefault(g => g.Id == id)?.Name ?? string.Empty;
        }

        ResetMessage();
    }

    protected async Task CreateGroup(Group group)
    {
        ResetMessage();

        if (string.IsNullOrEmpty(group.Name))
        {
            messageError = true;
            messageTop = "Bitte Namen eintragen.";
            return;
        }

        var result = await groupService.CreateGroupAsync(group.Name);
        if (!result.Success || result.Data == Guid.Empty)
        {
            messageError = true;
            messageTop = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Erstellen der Gruppe.";
            return;
        }

        groupNew = new();
        groupSelected = new();
        messageTop = "Gruppe erfolgreich angelegt.";

        await GetGroups();
        StateHasChanged();
    }

    protected async Task UpdateGroup(Group group)
    {
        ResetMessage();

        if (group.Id == Guid.Empty)
        {
            messageError = true;
            messageBottom = "Bitte Gruppe auswählen.";
            return;
        }

        if (string.IsNullOrEmpty(group.Name))
        {
            messageError = true;
            messageBottom = "Bitte neuen Namen vergeben.";
            return;
        }

        var result = await groupService.UpdateGroupAsync(group.Id, group.Name);
        if (!result.Success)
        {
            messageError = true;
            messageTop = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Updaten der Gruppe.";
            return;
        }

        messageBottom = "Gruppe erfolgreich geupdatet.";
        await GetGroups();
        groupSelected = groups.FirstOrDefault(x => x.Id == group.Id) ?? new();
        StateHasChanged();
    }

    protected async Task DeleteGroup(Guid id)
    {
        ResetMessage();

        if (id == Guid.Empty)
        {
            messageError = true;
            messageBottom = "Bitte Gruppe auswählen";
            return;
        }

        var result = await groupService.DeleteGroupAsync(id);
        if (!result.Success)
        {
            messageError = true;
            messageTop = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Löschen der Gruppe.";
            return;
        }

        messageBottom = "Gruppe erfolgreich gelöscht.";
        groupSelected = new();
        await GetGroups();
        StateHasChanged();
    }

    #region Helper
    private async Task GetGroups()
    {
        var result = await groupService.GetGroupsAsync();
        if (!result.Success || result.Data == null)
        {
            messageError = true;
            messageTop = !string.IsNullOrEmpty(result.Message) ? result.Message : "Fehler beim Laden der Gruppe.";
            return;
        }

        groups = result.Data;
    }

    private void ResetMessage()
    {
        messageError = false;
        messageTop = string.Empty;
        messageBottom = string.Empty;
    }
    #endregion
}