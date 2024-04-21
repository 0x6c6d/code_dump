namespace Web.Components;

public partial class AdminGroup
{
    private string ApiRoute = "Group";
    public string MessageTop = string.Empty;
    public string MessageBottom = string.Empty;
    public GroupModel GroupNew = new();
    public GroupModel GroupSelected = new();
    public List<GroupModel> Groups = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await GetGroups();

        if (firstRender)
            StateHasChanged();
    }

    private void HandleChangeEvent(Guid groupId)
    {
        GroupSelected.GroupId = groupId;
        GroupSelected.Name = Groups.FirstOrDefault(g => g.GroupId == groupId)?.Name ?? string.Empty;
    }

    protected async Task CreateGroup(GroupModel group)
    {
        MessageTop = string.Empty;
        MessageBottom = string.Empty;

        if (string.IsNullOrEmpty(group.Name))
        {
            MessageTop = "Bitte Namen eintragen.";
            return;
        }

        var result = await GroupService.CreateEntity(ApiRoute, group.Name);
        if (!result.Success || result.Data == Guid.Empty)
        {
            MessageTop = $"{result.Message} {(string.IsNullOrEmpty(result.ValidationErrors) ? "" : result.ValidationErrors)}";
            return;
        }

        MessageTop = "Gruppe erfolgreich angelegt.";
        await GetGroups();
        GroupNew = new();
        StateHasChanged();
    }

    protected async Task UpdateGroup(GroupModel group)
    {
        MessageTop = string.Empty;
        MessageBottom = string.Empty;

        if (group.GroupId == Guid.Empty)
        {
            MessageBottom = "Bitte Artikelgruppe auswählen.";
            return;
        }

        if (string.IsNullOrEmpty(group.Name))
        {
            MessageBottom = "Bitte neuen Namen vergeben.";
            return;
        }

        var result = await GroupService.UpdateEntity(ApiRoute, group.GroupId, group.Name);
        if (!result.Success)
        {
            MessageBottom = $"{result.Message} {(string.IsNullOrEmpty(result.ValidationErrors) ? "" : result.ValidationErrors)}";
            return;
        }

        MessageBottom = "Gruppe erfolgreich geupdatet.";
        await GetGroups();
        GroupSelected = Groups.FirstOrDefault(x => x.GroupId == group.GroupId) ?? new();
        StateHasChanged();
    }

    protected async Task DeleteGroup(Guid groupId)
    {
        MessageTop = string.Empty;
        MessageBottom = string.Empty;

        if (groupId == Guid.Empty)
        {
            MessageBottom = "Bitte Artikelgruppe auswählen";
            return;
        }

        var result = await GroupService.DeleteEntity(ApiRoute, groupId);
        if (!result.Success)
        {
            MessageBottom = $"{result.Message} {(string.IsNullOrEmpty(result.ValidationErrors) ? "" : result.ValidationErrors)}";
            return;
        }

        MessageBottom = "Gruppe erfolgreich gelöscht.";
        GroupSelected = new();
        await GetGroups();
        StateHasChanged();
    }

    private async Task GetGroups()
    {
        var result = await GroupService.GetEntities(ApiRoute);
        if (!result.Success)
        {
            MessageTop += $"\n{result.Message}";
            return;
        }

        Groups = result.Data ?? new();
    }
}