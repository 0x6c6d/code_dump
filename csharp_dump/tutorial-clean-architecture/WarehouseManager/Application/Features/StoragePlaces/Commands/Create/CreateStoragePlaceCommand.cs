namespace Application.Features.StoragePlaces.Commands.Create;
public class CreateStoragePlaceCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
}
