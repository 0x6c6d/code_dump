namespace Application.Features.StoreManager.Technologies.Operations.Create;
public class CreateTechnologyRequest : IRequest<string>
{
    public string StoreId { get; set; } = string.Empty;
    public string CashDeskName { get; set; } = string.Empty;
    public string InternetCustomerId { get; set; } = string.Empty;
}
