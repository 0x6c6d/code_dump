namespace Application.Features.StoreManager.Technologies.Operations.Read.One;
public class GetTechnologyRequest : IRequest<GetTechnologyReturn>
{
    public string StoreId { get; set; } = string.Empty;
}
