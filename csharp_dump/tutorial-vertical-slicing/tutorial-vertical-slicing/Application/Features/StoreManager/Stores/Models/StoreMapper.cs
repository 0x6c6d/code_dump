using Application.Features.StoreManager.Stores.Models;
using Application.Features.StoreManager.Stores.Operations.Create;
using Application.Features.StoreManager.Stores.Operations.Read.All;
using Application.Features.StoreManager.Stores.Operations.Read.One;
using Application.Features.StoreManager.Stores.Operations.Update;
using Riok.Mapperly.Abstractions;

namespace Application.Features.StoreManager.Stores;

[Mapper]
public static partial class StoreMapper
{
    // MediatR
    public static partial List<GetStoresReturn> StoreToGetStoresReturn(IOrderedEnumerable<Store> stores);

    public static partial Store CreateStoreRequestToStore(CreateStoreRequest createStoreRequest);

    public static partial void UpdateStoreRequestToStore(UpdateStoreRequest updateStoreRequest, Store store);

    // Business Logic

    public static partial List<Store> GetStoresFromGetStoresReturn(List<GetStoresReturn> getStoresReturn);

    public static partial GetStoreReturn StoreToGetStoreReturn(Store model);

    public static partial Store  GetStoreReturnToStore(GetStoreReturn @return);
}

[Mapper(UseDeepCloning = true)]
public static partial class StoreMapperDeepClone
{
    public static partial List<Store> StoresDeepClone(List<Store> model);
}