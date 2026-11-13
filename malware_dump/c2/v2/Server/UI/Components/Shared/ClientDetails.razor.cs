using Microsoft.AspNetCore.Components;
using Backend.Features.Command.Models;
using UI.Models;

namespace UI.Components.Shared;

public partial class ClientDetails
{
    [Parameter] public EventCallback<ClientViewModel> OnDelete { get; set; }
    
    private string? _message;
    private bool _showModal = false;
    private bool _showDeleteClientBtn = false;
    private bool _showDeleteCommandBtn = false;
    private ClientViewModel? _client;
    private List<CommandModel>? _commands;
    
    #region Button

    private void TriggerDeleteClientButton(bool showBtn)
    {
        _showDeleteClientBtn = showBtn;
        _showDeleteCommandBtn = false;
        StateHasChanged();
    }
    
    private void TriggerDeleteCommandButton(bool showBtn)
    {
        _showDeleteCommandBtn = showBtn;
        _showDeleteClientBtn = false;
        StateHasChanged();
    }

    private async Task DeleteClient()
    {
        if (_client == null)
        {
            _message = "Client model is empty.";
            return;
        }

        var client = ModelMapper.ClientViewModelToClient(_client);
        await clientService.DeleteClientAsync(client);
        await OnDelete.InvokeAsync(_client);
        
        CloseModal();
    }
    
    private async Task DeleteCommand(CommandModel model)
    {
        await commandService.DeleteCommandByIdAsync(model.Id);
        _commands?.Remove(model);
        StateHasChanged();
    }
    
    #endregion
    
    #region Helper
    
    private void CloseModal()
    {
        _showModal = false;
        StateHasChanged();
    }

    public async Task OpenModel(ClientViewModel model)
    {
        _client = model;
        _showModal = true;
        _showDeleteClientBtn = false;
        _showDeleteCommandBtn = false;
        StateHasChanged();

        if (string.IsNullOrEmpty(model.Ip))
        {
            _message = "Client ip is empty";
            return;
        }
        
        await FetchClientCommands(model.Ip);
    }
    
    private async Task FetchClientCommands(string ip)
    {
        _message = string.Empty;
        
        var result = await commandService.GetCommandPerClientAsync(ip);
        if (!result.Success)
        {
            _message = !string.IsNullOrEmpty(result.Message)
                ? result.Message
                : "Error while fetching all clients from the database";
            return;
        }

        if (result.Data == null || result.Data.Count == 0)
        {
            _message = "No commands found for this ip";
            return;
        }
        
        _commands = result.Data;
    }
    
    #endregion
    
    
}