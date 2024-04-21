using Microsoft.AspNetCore.Components;

namespace Web.Pages;

public partial class AddArticle
{
    string apiRouteGroup = "Group";
    string apiRouteStoragePlace = "StoragePlace";
    string apiRouteOperationArea = "OperationArea";
    public string Message = string.Empty;
    public ArticleVm ArticleVM { get; set; } = new();
    public List<GroupModel> Groups { get; set; } = new();
    public List<StoragePlaceModel> StoragePlaces { get; set; } = new();
    public List<OperationAreaModel> OperationAreas { get; set; } = new();
    public List<ProcurementModel> Procurements { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        var resultGroup = await GroupService.GetEntities(apiRouteGroup);
        Groups = resultGroup.Data ?? new();

        var resultStoragePlace = await StoragePlaceService.GetEntities(apiRouteStoragePlace);
        StoragePlaces = resultStoragePlace.Data ?? new();

        var resultOperationArea = await OperationAreaService.GetEntities(apiRouteOperationArea);
        OperationAreas = resultOperationArea.Data ?? new();

        var resultProcurements = await ProcurementService.GetProcurements();
        Procurements = resultProcurements.Data ?? new();
    }

    protected async Task HandleValidSubmit()
    {
        var article = Mappers.ArticelViewModelToArticleModel(ArticleVM);
        var result = await ArticleService.CreateArticle(article);
        if (!result.Success || result.Data == Guid.Empty)
        {
            Message = $"{result.Message} {(string.IsNullOrEmpty(result.ValidationErrors) ? "" : result.ValidationErrors)}";
            return;
        }

        NavigationManager.NavigateTo(NavigationManager.BaseUri);
        ArticleVM = new();
    }
}
