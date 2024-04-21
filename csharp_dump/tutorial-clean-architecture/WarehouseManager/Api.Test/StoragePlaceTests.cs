using Application.Contracts.Persistence;
using Application.Features.StoragePlaces.Commands.Create;
using Application.Features.StoragePlaces.Commands.Delete;
using Application.Features.StoragePlaces.Commands.Update;
using Application.Features.StoragePlaces.Queries.GetStoragePlace;
using Application.Features.StoragePlaces.Queries.GetStoragePlaces;
using Domain.Entities;
using Moq;

namespace Api.Test;

public class StoragePlaceTests
{
    private readonly GetStoragePlaceQueryHandler _sutGetStoragePlace;
    private readonly GetStoragePlacesQueryHandler _sutGetStoragePlaces;
    private readonly CreateStoragePlaceCommandHandler _sutCreateStoragePlace;
    private readonly DeleteStoragePlaceCommandHandler _sutDeleteStoragePlace;
    private readonly UpdateStoragePlaceCommandHandler _sutUpdateStoragePlace;
    private readonly Mock<IStoragePlaceRepository> _storagePlaceRepositoryMock = new();
    private readonly Mock<IArticleRepository> _articleRepositoryMock = new();
    

    public StoragePlaceTests()
    {
        _sutGetStoragePlace = new GetStoragePlaceQueryHandler(_storagePlaceRepositoryMock.Object);
        _sutGetStoragePlaces = new GetStoragePlacesQueryHandler(_storagePlaceRepositoryMock.Object);
        _sutCreateStoragePlace = new CreateStoragePlaceCommandHandler(_storagePlaceRepositoryMock.Object);
        _sutDeleteStoragePlace = new DeleteStoragePlaceCommandHandler(_storagePlaceRepositoryMock.Object, _articleRepositoryMock.Object);
        _sutUpdateStoragePlace = new UpdateStoragePlaceCommandHandler(_storagePlaceRepositoryMock.Object);
    }

    [Fact]
    public async Task GetStoragePlace_ShouldGetStoragePlace()
    {
        // Arrange
        var storagePlaceId = Guid.NewGuid();
        var getStoragePlaceQuery = new GetStoragePlaceQuery { StoragePlaceId = storagePlaceId };
        var storagePlace = new StoragePlace { StoragePlaceId = storagePlaceId, Name = "StoragePlace" };
        _storagePlaceRepositoryMock.Setup(x => x.GetByIdAsync(storagePlaceId)).ReturnsAsync(storagePlace);

        // Act
        var result = await _sutGetStoragePlace.Handle(getStoragePlaceQuery, new CancellationToken());

        // Assert
        _storagePlaceRepositoryMock.Verify(x => x.GetByIdAsync(storagePlaceId), Times.Once);

        Assert.NotNull(result);
        Assert.IsType<GetStoragePlaceVm>(result);
        Assert.Equal(storagePlace.StoragePlaceId, result.StoragePlaceId);
        Assert.Equal(storagePlace.Name, result.Name);
    }

    [Fact]
    public async Task GetStoragePlaces_ShouldGetListOfStoragePlaces()
    {
        // Arrange
        var storagePlaces = new List<StoragePlace>
        {
            new StoragePlace { StoragePlaceId = Guid.NewGuid(), Name = "StoragePlace1" },
            new StoragePlace { StoragePlaceId = Guid.NewGuid(), Name = "StoragePlace2" },
        };
        _storagePlaceRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(storagePlaces);

        // Act
        var result = await _sutGetStoragePlaces.Handle(new GetStoragePlacesQuery(), new CancellationToken());

        // Assert
        _storagePlaceRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);

        Assert.NotNull(result);
        Assert.IsType<List<GetStoragePlacesVm>>(result);
        Assert.Equal(storagePlaces.Count, result.Count);
        Assert.Equal(storagePlaces[0].Name, result[0].Name);
        Assert.Equal(storagePlaces[1].Name, result[1].Name);
    }

    [Fact]
    public async Task AddAsync_ShouldCreateStoragePlace()
    {
        // Arrange
        var createStoragePlaceCommand = new CreateStoragePlaceCommand { Name = "StoragePlace" };
        var returnValue = new StoragePlace { StoragePlaceId = Guid.NewGuid(), Name = createStoragePlaceCommand.Name };
        _storagePlaceRepositoryMock.Setup(x => x.AddAsync(It.IsAny<StoragePlace>())).ReturnsAsync(returnValue);

        // Act
        var result = await _sutCreateStoragePlace.Handle(createStoragePlaceCommand, new CancellationToken());

        // Assert
        _storagePlaceRepositoryMock.Verify(x => x.AddAsync(It.IsAny<StoragePlace>()), Times.Once);

        Assert.Equal(returnValue.StoragePlaceId, result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteStoragePlace()
    {
        // Arrange
        var storagePlaceId = Guid.NewGuid();
        var deleteStoragePlaceCommand = new DeleteStoragePlaceCommand { StoragePlaceId = storagePlaceId };
        var storagePlace = new StoragePlace { StoragePlaceId = storagePlaceId, Name = "StoragePlace" };
        _storagePlaceRepositoryMock.Setup(x => x.GetByIdAsync(storagePlaceId)).ReturnsAsync(storagePlace);
        _articleRepositoryMock.Setup(x => x.FindAnyArticleWithEntityId(It.IsAny<Func<Article, bool>>())).ReturnsAsync(false);

        // Act
        await _sutDeleteStoragePlace.Handle(deleteStoragePlaceCommand, new CancellationToken());

        // Assert
        _storagePlaceRepositoryMock.Verify(x => x.GetByIdAsync(storagePlaceId), Times.Once);
        _articleRepositoryMock.Verify(x => x.FindAnyArticleWithEntityId(It.IsAny<Func<Article, bool>>()), Times.Once);
        _storagePlaceRepositoryMock.Verify(x => x.DeleteAsync(storagePlace), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateStoragePlace()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var groupName = "Group";
        var updateStoragePlaceCommand = new UpdateStoragePlaceCommand
        {
            StoragePlaceId = groupId,
            Name = groupName
        };

        var storagePlace = new StoragePlace
        {
            StoragePlaceId = groupId,
            Name = groupName
        };

        _storagePlaceRepositoryMock.Setup(x => x.GetByIdAsync(groupId)).ReturnsAsync(storagePlace);

        // Act
        await _sutUpdateStoragePlace.Handle(updateStoragePlaceCommand, new CancellationToken());

        // Assert
        _storagePlaceRepositoryMock.Verify(x => x.GetByIdAsync(groupId), Times.Once);
        _storagePlaceRepositoryMock.Verify(x => x.UpdateAsync(storagePlace), Times.Once);
        Assert.Equal(updateStoragePlaceCommand.StoragePlaceId, storagePlace.StoragePlaceId);
        Assert.Equal(updateStoragePlaceCommand.Name, storagePlace.Name);
        Assert.NotEqual(default(DateTime), storagePlace.LastModifiedDate);
    }
}