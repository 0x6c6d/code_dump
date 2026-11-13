using System.Globalization;
using Backend.Features.Settings.Models;
using Microsoft.AspNetCore.Components;
using UI.Models;

namespace UI.Components.Pages;

public partial class Dashboard
{
    private string? _message;
    private string? _messageSettings;
    private string? _newCommand;
    private bool _allClientsChecked;
    private SettingsModel? _settings;
    private List<ClientViewModel>? _allClients;
    private List<ClientViewModel>? _filteredClients;
    private ClientDetails _clientDetailsRef = new();

    protected override async Task OnInitializedAsync()
    {
        await FetchSettings();
        await FetchClients();
    }

    #region Button

    private async Task RefreshDashboardButton()
    {
        await FetchSettings();
        await FetchClients();
        StateHasChanged();
    }

    private async Task ChangeCommandButton()
    {
        if (_allClients == null) return;

        foreach (var client in _allClients.Where(client => client.Checked))
        {
            client.Command = _newCommand;
            var clientModel = ModelMapper.ClientViewModelToClient(client);
            await clientService.UpdateClientAsync(clientModel);
        }

        _newCommand = string.Empty;
    }

    private async Task UpdateSettingsButton()
    {
        if (_settings == null)
        {
            _messageSettings = "Settings model is empty";
            return;
        }
        
        var result = await settingsService.UpdateSettingsAsync(_settings);
        if (!result.Success || !result.Data)
        {
            _messageSettings = !string.IsNullOrEmpty(result.Message)
                ? result.Message
                : "Error while updating the settings";
            return;
        }

        _messageSettings = "Updated settings successfully";
    }
    
    private async Task ShowClientDetailsButton(ClientViewModel model)
    {
        await _clientDetailsRef.OpenModel(model);
    }

    #endregion

    #region Helper

    private async Task FetchClients()
    {
        var result = await clientService.GetClientsAsync();
        if (!result.Success)
        {
            _message = !string.IsNullOrEmpty(result.Message)
                ? result.Message
                : "Error while fetching all clients from the database";
            return;
        }

        if (result.Data == null || result.Data.Count == 0)
        {
            _message = "No clients found in the database";
            return;
        }

        _allClients = ModelMapper.ClientsToClientViewModels(result.Data);
        _filteredClients = _allClients;
    }

    private async Task FetchSettings()
    {
        var result = await settingsService.GetSettingsAsync();
        if (!result.Success)
        {
            _message = !string.IsNullOrEmpty(result.Message)
                ? result.Message
                : "Error while fetching the settings from the database";
            return;
        }

        if (result.Data == null)
        {
            _message = "No settings found in the database";
            return;
        }

        _settings = result.Data;
    }
    
    private void FilterClients(ChangeEventArgs e)
    {
        var searchText = e.Value?.ToString();
        if (string.IsNullOrEmpty(searchText))
        {
            _filteredClients = _allClients;
        }
        else
        {
            _filteredClients = _allClients?.Where(client =>
                client is { Ip: not null, Command: not null } &&
                (client.Ip.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                 client.Port.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                 client.Command.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                 client.LastConnection.ToString(CultureInfo.InvariantCulture)
                     .Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                 client.LastModifiedDateTime.ToString(CultureInfo.InvariantCulture)
                     .Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                 client.CreatedDateTime.ToString(CultureInfo.InvariantCulture)
                     .Contains(searchText, StringComparison.OrdinalIgnoreCase))
            ).ToList();
        }
    }

    private bool AllClientsChecked
    {
        get => _allClientsChecked;
        set
        {
            if (_allClientsChecked != value)
            {
                _allClientsChecked = value;
                _allClients?.ForEach(client => client.Checked = _allClientsChecked);
            }
        }
    }
    
    private void HandleDelete(ClientViewModel model)
    {
        _allClients?.Remove(model);
        _filteredClients?.Remove(model);
        StateHasChanged();
    }

    #endregion
}