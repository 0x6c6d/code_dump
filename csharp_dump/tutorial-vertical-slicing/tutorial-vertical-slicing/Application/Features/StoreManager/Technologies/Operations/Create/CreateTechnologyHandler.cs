using Application.Features.StoreManager.Technologies.Repositories;

namespace Application.Features.StoreManager.Technologies.Operations.Create;
public class CreateTechnologyHandler : IRequestHandler<CreateTechnologyRequest, string>
{
    private readonly ITechnologyRepository _technologyRepository;

    public CreateTechnologyHandler(ITechnologyRepository technologyRepository)
    {
        _technologyRepository = technologyRepository;
    }

    public async Task<string> Handle(CreateTechnologyRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateTechnologyValidator(_technologyRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        var technology = TechnologyMapper.CreateTechnologyRequestToTechnology(request);
        technology.CreatedDate = DateTime.Now;
        technology.LastModifiedDate = DateTime.Now;
        technology = await _technologyRepository.AddAsync(technology);

        return technology.StoreId;
    }
}
