using Application.Features.StoreManager.Technologies.Models;
using Application.Features.StoreManager.Technologies.Operations.Create;
using Application.Features.StoreManager.Technologies.Operations.Read.One;
using Application.Features.StoreManager.Technologies.Operations.Update;
using Riok.Mapperly.Abstractions;

namespace Application.Features.StoreManager.Technologies;

[Mapper]
public static partial class TechnologyMapper
{
    // MediatR
    public static partial GetTechnologyReturn TechnologyToGetTechnologyReturn(Technology model);

    public static partial Technology CreateTechnologyRequestToTechnology(CreateTechnologyRequest request);

    public static partial void UpdateTechnologyRequestToTechnology(UpdateTechnologyRequest request, Technology model);

    // Business Logic
    public static partial Technology GetTechnologyReturnToTechnology(GetTechnologyReturn @return);

    public static partial CreateTechnologyRequest TechnologyToCreateTechnologyRequest(Technology model);

    public static partial UpdateTechnologyRequest TechnologyToUpdateTechnologyRequest(Technology model);

    public static partial TechnologyVm TechnologyToTechnologyVm(Technology model);

    public static partial Technology TechnologyVmToTechnology(TechnologyVm model);
}