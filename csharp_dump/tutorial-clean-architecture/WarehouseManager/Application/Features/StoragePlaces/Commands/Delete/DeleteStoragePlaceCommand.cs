namespace Application.Features.StoragePlaces.Commands.Delete;
public class DeleteStoragePlaceCommand : IRequest<Unit>
{
    public Guid StoragePlaceId { get; set; }
}
