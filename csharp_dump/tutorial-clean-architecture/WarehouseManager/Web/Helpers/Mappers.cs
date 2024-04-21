using Riok.Mapperly.Abstractions;

namespace Web.Helpers;

[Mapper]
public static partial class Mappers
{
    [MapProperty("GroupId", "Group.GroupId")]
    [MapProperty("OperationAreaId", "OperationArea.OperationAreaId")]
    [MapProperty("StoragePlaceId", "StoragePlace.StoragePlaceId")]
    [MapProperty("ProcurementId", "Procurement.ProcurementId")]
    public static partial ArticleModel ArticelViewModelToArticleModel(ArticleVm article);

    [MapProperty("Group.GroupId", "GroupId")]
    [MapProperty("OperationArea.OperationAreaId", "OperationAreaId")]
    [MapProperty("StoragePlace.StoragePlaceId", "StoragePlaceId")]
    [MapProperty("Procurement.ProcurementId", "ProcurementId")]
    public static partial ArticleVm ArticelModelToArticleViewModel(ArticleModel article);
}
