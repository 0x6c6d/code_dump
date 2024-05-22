using Application.Features.WarehouseManager.Articles;
using Application.Features.WarehouseManager.Articles.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Presentation.Components.Pages.WarehouseManager.Shared;

namespace Presentation.Components.Pages.WarehouseManager;

public partial class Main
{
    private bool expandAll = false;
    private bool sortTablesAscending = false;
    private string message = string.Empty;
    private Guid disposeId;
    private List<string> expandedCategories = new();
    private Details detailsRef = new();
    private ProcurementDetails procurementDetailsRef = new();
    private List<Article> allArticles = new();
    private List<Article> filteredArticles = new();
    private IEnumerable<IGrouping<string, Article>>? articlesWithCategory;
    private List<Article>? articlesWithoutCategory;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var result = await articleService.GetArticlesAsync();
            if (!result.Success || result.Data == null)
            {
                message = string.IsNullOrEmpty(result.Message) ? "Es wurden keine Artikel gefunden" : result.Message;
                StateHasChanged();
                return;
            }

            allArticles = result.Data;
            disposeId = allArticles.FirstOrDefault(x => x.Category.Name.Equals("Entsorgt", StringComparison.OrdinalIgnoreCase))?.CategoryId ?? Guid.Empty;
            filteredArticles = allArticles;
            SplitArticles();
            StateHasChanged();
        }
    }

    private async Task ShowDetails(Guid articleId)
    {
        var result = await articleService.GetArticleAsync(articleId);
        if (!result.Success || result.Data == null)
        {
            message = string.IsNullOrEmpty(result.Message) ? "Es wurden keine Artikel gefunden" : result.Message;
            return;
        }

        await detailsRef.OpenModal(result.Data);
    }

    private async Task ShowProcurementDetails(Guid? procurementId)
    {
        if (procurementId == null || procurementId == Guid.Empty)
        {
            message = "Der Lieferant hat eine ungültige Id.";
            return;
        }
        
        var result = await procurementService.GetProcurementAsync((Guid)procurementId);
        if (!result.Success || result.Data == null)
        {
            message = string.IsNullOrEmpty(result.Message) ? "Es wurden kein Lieferant gefunden" : result.Message;
            return;
        }

        procurementDetailsRef.OpenModal(result.Data);
    }

    private async Task UpdateStock(Guid id, int amount)
    {
        var result = await articleService.GetArticleAsync(id);
        if (!result.Success || result.Data == null)
        {
            message = string.IsNullOrEmpty(result.Message) ? "Es wurden keine Artikel gefunden" : result.Message;
            return;
        }

        var article = result.Data;
        article.Stock += amount;

        var resultUpdate = await articleService.UpdateArticleAsync(article);
        if (!resultUpdate.Success)
        {
            message = string.IsNullOrEmpty(result.Message) ? "Es wurden keine Artikel gefunden" : result.Message;
            return;
        }

        HandleUpdate(result.Data);
    }

    private void HandleUpdate(Article article)
    {
        // update list of articles manually to save a querry to the database
        var indexArticleList = allArticles.FindIndex(x => x.Id == article.Id);
        if (indexArticleList != -1)
            allArticles[indexArticleList] = article;

        var indexFilteredArticleList = filteredArticles.FindIndex(x => x.Id == article.Id);
        if (indexFilteredArticleList != -1)
            filteredArticles[indexFilteredArticleList] = article;

       SplitArticles(); 
    }

    #region Helper
    private void ToggleCategory(string category)
    {
        if (string.IsNullOrEmpty(category))
        {
            return;
        }

        if (expandedCategories.Contains(category))
        {
            expandedCategories.Remove(category);
        }
        else
        {
            expandedCategories.Add(category);
        }
    }

    private async Task AddArticleTemplate(string category)
    {
        var articleTemplate = filteredArticles.FirstOrDefault(a => a.Category?.Name == category);
        if (articleTemplate == null)
        {
            return;
        }

        var articleCopy = ArticleMapperDeepClone.ArticleDeepClone(articleTemplate);
        articleCopy.Id = Guid.NewGuid();
        articleCopy.Name = "Template";
        articleCopy.SerialNumber = string.Empty;
        articleCopy.InventoryNumber = string.Empty;
        articleCopy.ShoppingUrl = string.Empty;
        articleCopy.ImageUrl = string.Empty;
        articleCopy.Store = string.Empty;

        var result = await articleService.CreateArticleAsync(articleCopy);
        if (!result.Success || result.Data == Guid.Empty)
        {
            return;
        }

        allArticles.Add(articleCopy);
        SplitArticles();

        if (!expandedCategories.Contains(category))
        {
            expandedCategories.Add(category);
        }

        StateHasChanged();

        await ShowDetails(result.Data);
    }

    private void OpenShoppingUrl(string url)
    {
        jsRuntime.InvokeVoidAsync("window.open", url, "_blank");
    }

    private void SplitArticles()
    {
        // split articles in to list (articles with category & without category)
        articlesWithCategory = filteredArticles
            .Where(a => !a.Category.Name.Equals("Ohne Kategorie", StringComparison.CurrentCultureIgnoreCase))
            .GroupBy(a => a.Category.Name)
            .OrderBy(group => group.Key.Equals("Entsorgt", StringComparison.CurrentCultureIgnoreCase))
            .ThenBy(group => group.Key)
            .SelectMany(group => group.OrderBy(article => article.Name)
                                       .Select(article => new { Key = group.Key, Article = article }))
            .GroupBy(x => x.Key, x => x.Article);

        articlesWithoutCategory = filteredArticles
            .Where(a => a.Category.Name.Equals("Ohne Kategorie", StringComparison.CurrentCultureIgnoreCase))
            .OrderBy(a => a.Name)
            .ToList();
    }

    private void FilterArticles(ChangeEventArgs e)
    {
        expandAll = true;

        string searchTerm = e.Value?.ToString() ?? string.Empty;
        if (string.IsNullOrEmpty(searchTerm))
        {
            expandAll = false;
            filteredArticles = allArticles;
        }
        else
        {
            filteredArticles = allArticles.Where(article =>
                        article.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        article.SerialNumber.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        article.InventoryNumber.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        article.Group.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        article.Category.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        article.OperationArea.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        article.StoragePlace.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        article.Store.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                    ).ToList();
        }

        SplitArticles();
    }

    private void SortTableWithCategory(int columnIndex)
    {
        switch (columnIndex)
        {
            case 0:
                articlesWithCategory = sortTablesAscending ?
                    filteredArticles
                        .Where(a => !string.IsNullOrEmpty(a.Category.Name))
                        .OrderBy(a => a.Name)
                        .GroupBy(a => a.Category.Name)
                        .OrderBy(group => group.Key.Equals("Entsorgt", StringComparison.CurrentCultureIgnoreCase) ? 1 : 0)
                        .ThenBy(group => group.Key) :
                    filteredArticles
                        .Where(a => !string.IsNullOrEmpty(a.Category.Name))
                        .OrderByDescending(a => a.Name)
                        .GroupBy(a => a.Category.Name)
                        .OrderBy(group => group.Key.Equals("Entsorgt", StringComparison.CurrentCultureIgnoreCase) ? 1 : 0)
                        .ThenBy(group => group.Key);
                break;
            case 1:
                articlesWithCategory = sortTablesAscending ?
                    filteredArticles
                        .Where(a => !string.IsNullOrEmpty(a.Category.Name))
                        .OrderBy(a => a.SerialNumber)
                        .GroupBy(a => a.Category.Name)
                        .OrderBy(group => group.Key.Equals("Entsorgt", StringComparison.CurrentCultureIgnoreCase) ? 1 : 0)
                        .ThenBy(group => group.Key) :
                    filteredArticles
                        .Where(a => !string.IsNullOrEmpty(a.Category.Name))
                        .OrderByDescending(a => a.SerialNumber)
                        .GroupBy(a => a.Category.Name)
                        .OrderBy(group => group.Key.Equals("Entsorgt", StringComparison.CurrentCultureIgnoreCase) ? 1 : 0)
                        .ThenBy(group => group.Key);
                break;
            case 2:
                articlesWithCategory = sortTablesAscending ?
                    filteredArticles
                        .Where(a => !string.IsNullOrEmpty(a.Category.Name))
                        .OrderBy(a => a.InventoryNumber)
                        .GroupBy(a => a.Category.Name)
                        .OrderBy(group => group.Key.Equals("Entsorgt", StringComparison.CurrentCultureIgnoreCase) ? 1 : 0)
                        .ThenBy(group => group.Key) :
                    filteredArticles
                        .Where(a => !string.IsNullOrEmpty(a.Category.Name))
                        .OrderByDescending(a => a.InventoryNumber)
                        .GroupBy(a => a.Category.Name)
                        .OrderBy(group => group.Key.Equals("Entsorgt", StringComparison.CurrentCultureIgnoreCase) ? 1 : 0)
                        .ThenBy(group => group.Key);
                break;
            case 3:
                articlesWithCategory = sortTablesAscending ?
                    filteredArticles
                        .Where(a => !string.IsNullOrEmpty(a.Category.Name))
                        .OrderBy(a => a.OperationArea.Name)
                        .GroupBy(a => a.Category.Name)
                        .OrderBy(group => group.Key.Equals("Entsorgt", StringComparison.CurrentCultureIgnoreCase) ? 1 : 0)
                        .ThenBy(group => group.Key) :
                    filteredArticles
                        .Where(a => !string.IsNullOrEmpty(a.Category.Name))
                        .OrderByDescending(a => a.OperationArea.Name)
                        .GroupBy(a => a.Category.Name)
                        .OrderBy(group => group.Key.Equals("Entsorgt", StringComparison.CurrentCultureIgnoreCase) ? 1 : 0)
                        .ThenBy(group => group.Key);
                break;
            case 4:
                articlesWithCategory = sortTablesAscending ?
                    filteredArticles
                        .Where(a => !string.IsNullOrEmpty(a.Category.Name))
                        .OrderBy(a => a.StoragePlace.Name)
                        .GroupBy(a => a.Category.Name)
                        .OrderBy(group => group.Key.Equals("Entsorgt", StringComparison.CurrentCultureIgnoreCase) ? 1 : 0)
                        .ThenBy(group => group.Key) :
                    filteredArticles
                        .Where(a => !string.IsNullOrEmpty(a.Category.Name))
                        .OrderByDescending(a => a.StoragePlace.Name)
                        .GroupBy(a => a.Category.Name)
                        .OrderBy(group => group.Key.Equals("Entsorgt", StringComparison.CurrentCultureIgnoreCase) ? 1 : 0)
                        .ThenBy(group => group.Key);
                break;
            case 5:
                articlesWithCategory = sortTablesAscending ?
                    filteredArticles
                        .Where(a => !string.IsNullOrEmpty(a.Category.Name))
                        .OrderBy(a => a.Store)
                        .GroupBy(a => a.Category.Name)
                        .OrderBy(group => group.Key.Equals("Entsorgt", StringComparison.CurrentCultureIgnoreCase) ? 1 : 0)
                        .ThenBy(group => group.Key) :
                    filteredArticles
                        .Where(a => !string.IsNullOrEmpty(a.Category.Name))
                        .OrderByDescending(a => a.Store)
                        .GroupBy(a => a.Category.Name)
                        .OrderBy(group => group.Key.Equals("Entsorgt", StringComparison.CurrentCultureIgnoreCase) ? 1 : 0)
                        .ThenBy(group => group.Key);
                break;
        }

        sortTablesAscending = !sortTablesAscending;
    }

    private void SortTableWithoutCategory(int columnIndex)
    {
        switch (columnIndex)
        {
            case 0:
                articlesWithoutCategory?.Sort((a1, a2) => sortTablesAscending ? a1.Name.CompareTo(a2.Name) : a2.Name.CompareTo(a1.Name));
                break;
            case 1:
                articlesWithoutCategory?.Sort((a1, a2) => sortTablesAscending ? a1.OperationArea.Name.CompareTo(a2.OperationArea.Name) : a2.OperationArea.Name.CompareTo(a1.OperationArea.Name));
                break;
            case 2:
                articlesWithoutCategory?.Sort((a1, a2) => sortTablesAscending ? a1.StoragePlace.Name.CompareTo(a2.StoragePlace.Name) : a2.StoragePlace.Name.CompareTo(a1.StoragePlace.Name));
                break;
            case 3:
                articlesWithoutCategory?.Sort((a1, a2) => sortTablesAscending ? a1.Group.Name.CompareTo(a2.Group.Name) : a2.Group.Name.CompareTo(a1.Group.Name));
                break;
            case 4:
                articlesWithoutCategory?.Sort((a1, a2) => sortTablesAscending ? a1.Stock.CompareTo(a2.Stock) : a2.Stock.CompareTo(a1.Stock));
                break;
            case 5:
                articlesWithoutCategory?.Sort((a1, a2) => sortTablesAscending ? a1.MinStock.CompareTo(a2.MinStock) : a2.MinStock.CompareTo(a1.MinStock));
                break;
        }

        sortTablesAscending = !sortTablesAscending;
    }
    #endregion
}
