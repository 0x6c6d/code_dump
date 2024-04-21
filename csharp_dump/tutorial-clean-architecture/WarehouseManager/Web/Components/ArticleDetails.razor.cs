using Microsoft.AspNetCore.Components;

namespace Web.Components;

public partial class ArticleDetails
{
    [Parameter]
    public EventCallback<ArticleModel> OnUpdate { get; set; }
    [Parameter]
    public EventCallback<Guid> OnDelete { get; set; }

    private bool ShowModal = false;
    private bool ShowDeletePage = false;

    private string ApiRouteGroup = "Group";
    private string ApiRouteStoragePlace = "StoragePlace";
    private string ApiRouteOperationArea = "OperationArea";
    private string Message = string.Empty;
    public ArticleVm ArticleVM { get; set; } = new();
    public List<GroupModel> Groups { get; set; } = new();
    public List<StoragePlaceModel> StoragePlaces { get; set; } = new();
    public List<OperationAreaModel> OperationAreas { get; set; } = new();
    public List<ProcurementModel> Procurements { get; set; } = new();

    public void OpenModal(ArticleModel article)
    {
        ArticleVM = Mappers.ArticelModelToArticleViewModel(article);
        ShowModal = true;
        StateHasChanged();
    }

    private void CloseModal()
    {
        ShowDeletePage = false;
        ShowModal = false;
        StateHasChanged();
    }

    protected override async Task OnInitializedAsync()
    {
        var resultGroup = await GroupService.GetEntities(ApiRouteGroup);
        Groups = resultGroup.Data ?? new();

        var resultStoragePlace = await StoragePlaceService.GetEntities(ApiRouteStoragePlace);
        StoragePlaces = resultStoragePlace.Data ?? new();

        var resultOperationArea = await OperationAreaService.GetEntities(ApiRouteOperationArea);
        OperationAreas = resultOperationArea.Data ?? new();

        var resultProcurements = await ProcurementService.GetProcurements();
        Procurements = resultProcurements.Data ?? new();
    }

    protected async Task HandleValidSubmit()
    {
        var article = Mappers.ArticelViewModelToArticleModel(ArticleVM);
        var result = await ArticleService.UpdateArticle(article);
        if (!result.Success)
        {
            Message = $"{result.Message} {(string.IsNullOrEmpty(result.ValidationErrors) ? "" : result.ValidationErrors)}";
            return;
        }

        var resultUpdate = await ArticleService.GetArticle(article.ArticleId);
        if (!resultUpdate.Success)
        {
            Message = $"{resultUpdate.Message} {(string.IsNullOrEmpty(resultUpdate.ValidationErrors) ? "" : result.ValidationErrors)}";
            return;
        }

        await OnUpdate.InvokeAsync(resultUpdate.Data);
        CloseModal();
    }

    private async void HandleDelete()
    {
        await ArticleService.DeleteArticle(ArticleVM.ArticleId);
        await OnDelete.InvokeAsync(ArticleVM.ArticleId);
        CloseModal();
    }
}