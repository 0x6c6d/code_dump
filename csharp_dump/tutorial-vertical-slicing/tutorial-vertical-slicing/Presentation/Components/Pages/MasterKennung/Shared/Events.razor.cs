using Application.Features.StoreManager.Events;
using Application.Features.StoreManager.Events.Models;

namespace Presentation.Components.Pages.MasterKennung.Shared;

public partial class Events
{
    private bool showModal = false;
    private bool messageError = false;
    private string message = string.Empty;
    private string storeId = string.Empty;
    private Guid editableEventId = Guid.Empty;
    private List<EventType> eventTypes = new();
    private EventVm eventVm = new();
    private EventVm editedEventVm = new();
    private List<Event> events = new();

    private async Task LoadEventTypes()
    {
        var result = await eventService.GetEventTypesAsync();
        if (result == null || !result.Success || result.Data == null)
        {
            messageError = true;
            message = string.IsNullOrEmpty(result?.Message) ? "Es ist ein Fehler beim Holen der Terminarten aufgetreten." : result.Message;
        }

        eventTypes = result?.Data ?? new();
        if (eventTypes.Count == 0)
        {
            messageError = false;
            message = $"Keine Terminarten gefunden.";
        }
    }

    private async Task LoadEvents()
    {
        var result = await eventService.GetEventsPerStoreAsync(storeId);
        if (result == null || !result.Success || result.Data == null)
        {
            messageError = true;
            message = string.IsNullOrEmpty(result?.Message) ? "Es ist ein Fehler beim Holen der Termine aufgetreten." : result.Message;
        }

        events = result?.Data ?? new();
        if (events.Count == 0)
        {
            messageError = false;
            message = $"Keine Termine für die Filiale '{storeId}' gefunden.";
        }
    }

    private async Task HandleValidSubmit()
    {
        message = string.Empty;
        messageError = false;

        var @event = EventMapper.EventVmToEvent(eventVm);
        var result = await eventService.CreateEventAsync(@event);
        if (result == null || !result.Success || result.Data == Guid.Empty)
        {
            messageError = true;
            message = string.IsNullOrEmpty(result?.Message) ? "Es ist ein Fehler beim Erstellen des Termines aufgetreten." : result.Message;
            return;
        }

        await LoadEvents();
    }

    private async Task HandleDelete(Guid id)
    {
        var result = await eventService.DeleteEventAsync(id);
        if (result == null || !result.Success || !result.Data)
        {
            messageError = true;
            message = string.IsNullOrEmpty(result?.Message) ? "Es ist ein Fehler beim Holen der Termine aufgetreten." : result.Message;
            return;
        }

        var deletedEvent = events.FirstOrDefault(x => x.Id == id);
        if (deletedEvent != null)
        {
            events.Remove(deletedEvent);
            StateHasChanged();
        }
    }

    private async Task HandleChange()
    {
        var @event = EventMapper.EventVmToEvent(editedEventVm);
        var result = await eventService.UpdateEventAsync(@event);
        if (result == null || !result.Success || !result.Data)
        {
            messageError = true;
            message = string.IsNullOrEmpty(result?.Message) ? "Es ist ein Fehler beim Bearbeiten des Termines aufgetreten." : result.Message;
            return;
        }

        editableEventId = Guid.Empty;
        await LoadEvents();
    }

    #region Helper
    public async void OpenModal(string id)
    {
        messageError = false;
        message = string.Empty;
        storeId = id;
        editableEventId = Guid.Empty;
        eventVm = new()
        {
            StoreId = storeId,
            Date = DateOnly.FromDateTime(DateTime.Today)
        };
        showModal = true;

        StateHasChanged();
        await LoadEventTypes();
        StateHasChanged();
        await LoadEvents();
        StateHasChanged();
    }

    private void CloseModal()
    {       
        showModal = false;
        StateHasChanged();
    }

    private void SwitchToChange(Guid id)
    {
        editableEventId = id;
        if (id == Guid.Empty)
        {
            return;
        }
        
        var @event = events.FirstOrDefault(x => x.Id.Equals(id));
        if (@event == null || @event.Id == Guid.Empty)
        {
            messageError = true;
            message = "Fehler beim Bearbeiten des Termins";
            return;
        }
        
        editedEventVm = new()
        {
            Id = @event.Id,
            EventId = @event.EventId,
            StoreId = @event.StoreId,
            Date = @event.Date,
            Time = @event.Time,
        };

        StateHasChanged();
    }
    #endregion
}
