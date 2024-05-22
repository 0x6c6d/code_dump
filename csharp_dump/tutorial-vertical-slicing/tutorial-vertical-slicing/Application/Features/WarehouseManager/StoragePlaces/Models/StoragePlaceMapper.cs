using Application.Features.WarehouseManager.StoragePlaces.Models;
using Application.Features.WarehouseManager.StoragePlaces.Operations.Create;
using Application.Features.WarehouseManager.StoragePlaces.Operations.Read.All;
using Application.Features.WarehouseManager.StoragePlaces.Operations.Read.One;
using Application.Features.WarehouseManager.StoragePlaces.Operations.Update;
using Riok.Mapperly.Abstractions;

namespace Application.Features.WarehouseManager.StoragePlaces;

[Mapper]
public static partial class StoragePlaceMapper
{
    // MediatR
    public static partial GetStoragePlaceReturn StoragePlaceToGetStoragePlaceReturn(StoragePlace storagePlace);
    
    public static partial List<GetStoragePlacesReturn> StoragePlacesToGetStoragePlacesReturn(IOrderedEnumerable<StoragePlace> storagePlaces);

    public static partial StoragePlace CreateStoragePlaceRequestToStoragePlace(CreateStoragePlaceRequest request);

    public static partial void UpdateStoragePlaceRequestToStoragePlace(UpdateStoragePlaceRequest request, StoragePlace storagePlace);

    // Business Logic
    public static partial StoragePlace GetStoragePlaceFromGetStoragePlaceReturn(GetStoragePlaceReturn getStoragePlaceReturn);

    public static partial List<StoragePlace> GetStoragePlacesFromGetStoragePlacesReturn(List<GetStoragePlacesReturn> getStoragePlacesReturn);
}
