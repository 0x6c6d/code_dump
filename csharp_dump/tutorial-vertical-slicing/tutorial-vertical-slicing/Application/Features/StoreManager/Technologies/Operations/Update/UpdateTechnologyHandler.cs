using Application.Features.StoreManager.Technologies.Models;
using Application.Features.StoreManager.Technologies.Repositories;

namespace Application.Features.StoreManager.Technologies.Operations.Update;
public class UpdateTechnologyHandler : IRequestHandler<UpdateTechnologyRequest, Unit>
{
    private readonly ITechnologyRepository _technologyRepository;

    public UpdateTechnologyHandler(ITechnologyRepository technologyRepository)
    {
        _technologyRepository = technologyRepository;
    }

    public async Task<Unit> Handle(UpdateTechnologyRequest request, CancellationToken cancellationToken)
    {
        var technology = await _technologyRepository.GetByStoreIdAsync(request.StoreId);
        if (technology == null)
            throw new NotFoundException(nameof(Technology), request.StoreId);

        var validator = new UpdateTechnologyValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        TechnologyMapper.UpdateTechnologyRequestToTechnology(request, technology);
        technology.LastModifiedDate = DateTime.Now;
        await _technologyRepository.UpdateAsync(technology);

        return Unit.Value;
    }
}
