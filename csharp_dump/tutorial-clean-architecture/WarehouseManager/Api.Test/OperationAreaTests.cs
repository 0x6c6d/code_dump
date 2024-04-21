using Application.Contracts.Persistence;
using Application.Features.OperationAreas.Commands.Create;
using Application.Features.OperationAreas.Commands.Delete;
using Application.Features.OperationAreas.Commands.Update;
using Application.Features.OperationAreas.Queries.GetOperationArea;
using Application.Features.OperationAreas.Queries.GetOperationAreas;
using Domain.Entities;
using Moq;

namespace Api.Test;

public class OperationAreaTests
{
    private readonly GetOperationAreaQueryHandler _sutGetOperationArea;
    private readonly GetOperationAreasQueryHandler _sutGetOperationAreas;
    private readonly CreateOperationAreaCommandHandler _sutCreateOperationArea;
    private readonly DeleteOperationAreaCommandHandler _sutDeleteOperationArea;
    private readonly UpdateOperationAreaCommandHandler _sutUpdateOperationArea;
    private readonly Mock<IOperationAreaRepository> _operationAreaRepositoryMock = new();
    private readonly Mock<IArticleRepository> _articleRepositoryMock = new();
    

    public OperationAreaTests()
    {
        _sutGetOperationArea = new GetOperationAreaQueryHandler(_operationAreaRepositoryMock.Object);
        _sutGetOperationAreas = new GetOperationAreasQueryHandler(_operationAreaRepositoryMock.Object);
        _sutCreateOperationArea = new CreateOperationAreaCommandHandler(_operationAreaRepositoryMock.Object);
        _sutDeleteOperationArea = new DeleteOperationAreaCommandHandler(_operationAreaRepositoryMock.Object, _articleRepositoryMock.Object);
        _sutUpdateOperationArea = new UpdateOperationAreaCommandHandler(_operationAreaRepositoryMock.Object);
    }

    [Fact]
    public async Task GetOperationArea_ShouldGetOperationArea()
    {
        // Arrange
        var operationAreaId = Guid.NewGuid();
        var getOperationAreaQuery = new GetOperationAreaQuery { OperationAreaId = operationAreaId };
        var operationArea = new OperationArea { OperationAreaId = operationAreaId, Name = "OperationArea" };
        _operationAreaRepositoryMock.Setup(x => x.GetByIdAsync(operationAreaId)).ReturnsAsync(operationArea);

        // Act
        var result = await _sutGetOperationArea.Handle(getOperationAreaQuery, new CancellationToken());

        // Assert
        _operationAreaRepositoryMock.Verify(x => x.GetByIdAsync(operationAreaId), Times.Once);

        Assert.NotNull(result);
        Assert.IsType<GetOperationAreaVm>(result);
        Assert.Equal(operationArea.OperationAreaId, result.OperationAreaId);
        Assert.Equal(operationArea.Name, result.Name);
    }

    [Fact]
    public async Task GetOperationAreas_ShouldGetListOfOperationAreas()
    {
        // Arrange
        var operationAreas = new List<OperationArea>
        {
            new OperationArea { OperationAreaId = Guid.NewGuid(), Name = "OperationArea1" },
            new OperationArea { OperationAreaId = Guid.NewGuid(), Name = "OperationArea2" },
        };
        _operationAreaRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(operationAreas);

        // Act
        var result = await _sutGetOperationAreas.Handle(new GetOperationAreasQuery(), new CancellationToken());

        // Assert
        _operationAreaRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);

        Assert.NotNull(result);
        Assert.IsType<List<GetOperationAreasVm>>(result);
        Assert.Equal(operationAreas.Count, result.Count);
        Assert.Equal(operationAreas[0].Name, result[0].Name);
        Assert.Equal(operationAreas[1].Name, result[1].Name);
    }

    [Fact]
    public async Task AddAsync_ShouldCreateOperationArea()
    {
        // Arrange
        var createOperationAreaCommand = new CreateOperationAreaCommand { Name = "OperationArea" };
        var returnValue = new OperationArea { OperationAreaId = Guid.NewGuid(), Name = createOperationAreaCommand.Name };
        _operationAreaRepositoryMock.Setup(x => x.AddAsync(It.IsAny<OperationArea>())).ReturnsAsync(returnValue);

        // Act
        var result = await _sutCreateOperationArea.Handle(createOperationAreaCommand, new CancellationToken());

        // Assert
        _operationAreaRepositoryMock.Verify(x => x.AddAsync(It.IsAny<OperationArea>()), Times.Once);

        Assert.Equal(returnValue.OperationAreaId, result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteOperationArea()
    {
        // Arrange
        var operationAreaId = Guid.NewGuid();
        var deleteOperationAreaCommand = new DeleteOperationAreaCommand { OperationAreaId = operationAreaId };
        var operationArea = new OperationArea { OperationAreaId = operationAreaId, Name = "Group" };
        _operationAreaRepositoryMock.Setup(x => x.GetByIdAsync(operationAreaId)).ReturnsAsync(operationArea);
        _articleRepositoryMock.Setup(x => x.FindAnyArticleWithEntityId(It.IsAny<Func<Article, bool>>())).ReturnsAsync(false);

        // Act
        await _sutDeleteOperationArea.Handle(deleteOperationAreaCommand, new CancellationToken());

        // Assert
        _operationAreaRepositoryMock.Verify(x => x.GetByIdAsync(operationAreaId), Times.Once);
        _articleRepositoryMock.Verify(x => x.FindAnyArticleWithEntityId(It.IsAny<Func<Article, bool>>()), Times.Once);
        _operationAreaRepositoryMock.Verify(x => x.DeleteAsync(operationArea), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateOperationArea()
    {
        // Arrange
        var operationAreaId = Guid.NewGuid();
        var operationAreaName = "OperationArea";
        var updateOperationAreaCommand = new UpdateOperationAreaCommand
        {
            OperationAreaId = operationAreaId,
            Name = operationAreaName
        };

        var operationArea = new OperationArea
        {
            OperationAreaId = operationAreaId,
            Name = operationAreaName
        };

        _operationAreaRepositoryMock.Setup(x => x.GetByIdAsync(operationAreaId)).ReturnsAsync(operationArea);

        // Act
        await _sutUpdateOperationArea.Handle(updateOperationAreaCommand, new CancellationToken());

        // Assert
        _operationAreaRepositoryMock.Verify(x => x.GetByIdAsync(operationAreaId), Times.Once);
        _operationAreaRepositoryMock.Verify(x => x.UpdateAsync(operationArea), Times.Once);
        Assert.Equal(updateOperationAreaCommand.OperationAreaId, operationArea.OperationAreaId);
        Assert.Equal(updateOperationAreaCommand.Name, operationArea.Name);
        Assert.NotEqual(default(DateTime), operationArea.LastModifiedDate);
    }
}