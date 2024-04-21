using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Web.Components;

namespace Web.Pages;

public partial class Dashboard
{
    private int Counter;
    public bool AuthState = false;
    private string Message = string.Empty;
    private string SearchTerm = string.Empty;
    private ArticleDetails ArticleDetailsRef = new();
    private List<ArticleModel> Articles = new();
    private List<ArticleModel> FilteredArticles
    {
        get
        {
            Counter = 0;
            
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                return Articles;
            }
            else
            {
                return Articles.Where(article =>
                    article.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    article.ItemNumber.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    article.StorageBin.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    article.Group.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    article.OperationArea.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    article.StoragePlace.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    article.Procurement.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }
        }
    }

    protected override async Task OnInitializedAsync()
    {
        AuthState = AuthService.AuthState;
        if (!AuthState)
            NavigationManager.NavigateTo("login");

        var result = await ArticleService.GetArticles();
        if (!result.Success)
        {
            Message = $"{result.Message} {(string.IsNullOrEmpty(result.ValidationErrors) ? "" : result.ValidationErrors)}";
            return;
        }

        Articles = result.Data ?? new();
    }

    private async Task ShowDetails(Guid articleId)
    {
        var result = await ArticleService.GetArticle(articleId);
        if (!result.Success || result.Data == null)
        {
            Message = $"{result.Message} {(string.IsNullOrEmpty(result.ValidationErrors) ? "" : result.ValidationErrors)}";
            return;
        }

        ArticleDetailsRef.OpenModal(result.Data);
    }

    private async Task UpdateStock(Guid articleId, int amount)
    {
        var result = await ArticleService.GetArticle(articleId);
        if (!result.Success || result.Data == null)
        {
            Message = $"{result.Message} {(string.IsNullOrEmpty(result.ValidationErrors) ? "" : result.ValidationErrors)}";
            return;
        }

        result.Data.Stock += amount;

        var resultUpdate = await ArticleService.UpdateArticle(result.Data);
        if (!resultUpdate.Success)
        {
            Message = $"{resultUpdate.Message} {(string.IsNullOrEmpty(resultUpdate.ValidationErrors) ? "" : resultUpdate.ValidationErrors)}";
            return;
        }

        HandleUpdate(result.Data);
    }

    private void OpenShoppingUrl(string url)
    {
        JSRuntime.InvokeVoidAsync("window.open", url, "_blank");
    }

    private void HandleUpdate(ArticleModel article)
    {
        // update list of articles manually to save a querry to the database
        var indexArticles = Articles.FindIndex(x => x.ArticleId == article.ArticleId);
        if (indexArticles != -1)
            Articles[indexArticles] = article;

        StateHasChanged();
    }

    private void HandleDelete(Guid articleId)
    {
        // update list of articles manually to save a querry to the database
        var indexArticles = Articles.FindIndex(x => x.ArticleId == articleId);
        if (indexArticles != -1)
            Articles.RemoveAt(indexArticles);
    }
}
