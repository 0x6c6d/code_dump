using Application.Features.StoreManager.Events.Respositories;

namespace Application.Features.StoreManager.Events.Operations.Event.Create;
public class CreateEventValidator : AbstractValidator<CreateEventRequest>
{
    private readonly IEventRepository _eventRepository;

    public CreateEventValidator(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;

        RuleFor(s => s.StoreId)
            .NotEmpty().WithMessage("{PropertyName} wird benötigt.")
            .NotNull()
            .Length(6, 6).WithMessage("{PropertyName} muss 6 Zeichen lang sein.");

        RuleFor(a => a)
          .MustAsync(EventIdUnique)
          .WithMessage("Ein Termin mit denselben Daten existiert bereits.");
    }

    private async Task<bool> EventIdUnique(CreateEventRequest e, CancellationToken token)
    {
        return !await _eventRepository.IsEventUnique(e.EventId, e.Date, e.Time);
    }
}
