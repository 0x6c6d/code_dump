using Application.Features.Articles.Commands.Create;
using Application.Features.Articles.Commands.Update;
using Application.Features.Articles.Queries.GetArticle;
using Application.Features.Articles.Queries.GetArticles;
using Application.Features.Groups.Commands.Create;
using Application.Features.Groups.Commands.Update;
using Application.Features.Groups.Queries.GetGroup;
using Application.Features.Groups.Queries.GetGroups;
using Application.Features.OperationAreas.Commands.Create;
using Application.Features.OperationAreas.Commands.Update;
using Application.Features.OperationAreas.Queries.GetOperationArea;
using Application.Features.OperationAreas.Queries.GetOperationAreas;
using Application.Features.Procurements.Commands.Create;
using Application.Features.Procurements.Commands.Update;
using Application.Features.Procurements.Queries.GetProcurement;
using Application.Features.Procurements.Queries.GetProcurements;
using Application.Features.StoragePlaces.Commands.Create;
using Application.Features.StoragePlaces.Commands.Update;
using Application.Features.StoragePlaces.Queries.GetStoragePlace;
using Application.Features.StoragePlaces.Queries.GetStoragePlaces;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper;

[Mapper]
public static partial class Mappers
{
    // Article
    public static partial GetArticleVm ArticleToGetArticleVm(Article article);
    public static partial GetArticlesVm ArticleToGetArticlesVm(Article article);
    public static partial Article CreateArticleCommandToArticle(CreateArticleCommand createArticleCommand);
    public static partial void UpdateArticleCommandToArticle(UpdateArticleCommand updateArticleCommand, Article article);

    // Group
    public static partial GetGroupVm GroupToGetGroupVm(Group group);
    public static partial List<GetGroupsVm> GroupsToGetGroupsVm(IOrderedEnumerable<Group> groups);
    public static partial Group CreateGroupCommandToGroup(CreateGroupCommand createGroupCommand);
    public static partial void UpdateGroupCommandToGroup(Features.Groups.Commands.Update.UpdateGroupCommand updateGroupCommand, Group group);

    // OperationArea
    public static partial GetOperationAreaVm OperationAreaToGetOperationAreaVm(OperationArea operationArea);
    public static partial List<GetOperationAreasVm> OperationAreasToGetOperationAreasVm(IOrderedEnumerable<OperationArea> operationAreas);
    public static partial OperationArea CreateOperationAreaCommandToOperationArea(CreateOperationAreaCommand createOperationAreaCommand);
    public static partial void UpdateOperationAreaCommandToOperationArea(Features.OperationAreas.Commands.Update.UpdateOperationAreaCommand updateOperationAreaCommand, OperationArea operationArea);

    // Procurement
    public static partial GetProcurementVm ProcurementToGetProcurementVm(Procurement procurement);
    public static partial List<GetProcurementsVm> ProcurementToGetProcurementsVm(IOrderedEnumerable<Procurement> procurements);
    public static partial Procurement CreateProcurementCommandToAProcurement(CreateProcurementCommand createProcurementCommand);
    public static partial void UpdateProcurementCommandToProcurement(UpdateProcurementCommand updateProcurementCommand, Procurement procurement);

    // StoragePlace
    public static partial GetStoragePlaceVm StoragePlaceToGetStoragePlaceVm(StoragePlace storagePlace);
    public static partial List<GetStoragePlacesVm> StoragePlacesToGetStoragePlacesVm(IOrderedEnumerable<StoragePlace> storagePlaces);
    public static partial StoragePlace CreateStoragePlaceCommandToStoragePlace(CreateStoragePlaceCommand createStoragePlaceCommand);
    public static partial void UpdateStoragePlaceCommandToStoragePlace(UpdateStoragePlaceCommand updateStoragePlaceCommand, StoragePlace storagePlace);
}