using Application.Features.WarehouseManager.Articles.Models;
using Application.Features.WarehouseManager.Articles.Operations.Create;
using Application.Features.WarehouseManager.Articles.Operations.Read.All;
using Application.Features.WarehouseManager.Articles.Operations.Read.One;
using Application.Features.WarehouseManager.Articles.Operations.Update;
using Riok.Mapperly.Abstractions;

namespace Application.Features.WarehouseManager.Articles;

[Mapper]
public static partial class ArticleMapper
{
    // MediatR
    public static partial GetArticleReturn ArticleToGetArticleReturn(Article article);

    public static partial GetArticlesReturn ArticleToGetArticlesReturn(Article article);

    public static partial Article CreateArticleRequestToArticle(CreateArticleRequest createArticleRequest);

    public static partial void UpdateArticleRequestToArticle(UpdateArticleRequest updateArticleRequest, Article article);

    // Business Logic
    public static partial ArticleVm ArticleToArticleVm(Article article);

    [MapProperty("GroupId", "Group.Id")]
    [MapProperty("CategoryId", "Category.Id")]
    [MapProperty("OperationAreaId", "OperationArea.Id")]
    [MapProperty("StoragePlaceId", "StoragePlace.Id")]
    [MapProperty("ProcurementId", "Procurement.Id")]
    public static partial Article ArticleVmToArticle(ArticleVm article);

    public static partial Article GetArticleFromGetArticleReturn(GetArticleReturn getArticleReturn);

    public static partial List<Article> GetArticlesFromGetArticlesReturn(List<GetArticlesReturn> getArticlesReturn);

    public static partial CreateArticleRequest CreateArticleRequestFromArticle(Article article);

    public static partial UpdateArticleRequest UpdateArticleRequestFromArticle(Article article);
}

[Mapper(UseDeepCloning = true)]
public static partial class ArticleMapperDeepClone
{
    public static partial Article ArticleDeepClone(Article article);
}
