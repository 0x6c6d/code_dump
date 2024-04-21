using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Features.Articles.Commands.Create;
using Application.Features.Articles.Commands.Delete;
using Application.Features.Articles.Commands.Update;
using Application.Features.Articles.Queries.GetArticle;
using Application.Features.Articles.Queries.GetArticles;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Api.Test;

public class ArticleTests
{
    private readonly GetArticleQueryHandler _sutGetArticle;
    private readonly GetArticlesQueryHandler _sutGetArticles;
    private readonly CreateArticleCommandHandler _sutCreateArticle;
    private readonly DeleteArticleCommandHandler _sutDeleteArticle;
    private readonly UpdateArticleCommandHandler _sutUpdateArticle;
    private readonly Mock<IArticleRepository> _articleRepositoryMock = new();
    private readonly Mock<IGroupRepository> _groupRepositoryMock = new();
    private readonly Mock<IOperationAreaRepository> _operationAreaRepositoryMock = new();
    private readonly Mock<IProcurementRepository> _procurementRepositoryMock = new();
    private readonly Mock<IStoragePlaceRepository> _storagePlaceRepositoryMock = new();
    private readonly Mock<IEmailService> _emailServiceMock = new();
    private readonly Mock<IConfiguration> _configurationMock = new();
    

    public ArticleTests()
    {
        _sutGetArticle = new GetArticleQueryHandler(_articleRepositoryMock.Object, _groupRepositoryMock.Object, _operationAreaRepositoryMock.Object, _storagePlaceRepositoryMock.Object, _procurementRepositoryMock.Object);
        _sutGetArticles = new GetArticlesQueryHandler(_articleRepositoryMock.Object, _groupRepositoryMock.Object, _operationAreaRepositoryMock.Object, _storagePlaceRepositoryMock.Object, _procurementRepositoryMock.Object);
        _sutCreateArticle = new CreateArticleCommandHandler(_articleRepositoryMock.Object);
        _sutDeleteArticle = new DeleteArticleCommandHandler(_articleRepositoryMock.Object);
        _sutUpdateArticle = new UpdateArticleCommandHandler(_configurationMock.Object, _emailServiceMock.Object, _articleRepositoryMock.Object);
    }

    [Fact]
    public async Task GetArticle_ShouldGetArticle()
    {
        // Arrange
        var articleId = Guid.NewGuid();
        var groupId = Guid.NewGuid();
        var operationAreaId = Guid.NewGuid();
        var procurementId = Guid.NewGuid();
        var storagePlaceId = Guid.NewGuid();

        var getArticleQuery = new GetArticleQuery { ArticleId = articleId };
        var group = new Group { GroupId = groupId, Name = "Group" };
        var operationArea = new OperationArea { OperationAreaId = operationAreaId, Name = "OperationArea" };
        var procurement = new Procurement { ProcurementId = procurementId, Name = "Procurement" };
        var storagePlace = new StoragePlace { StoragePlaceId = storagePlaceId, Name = "StoragePlace" };
        var article = new Article 
        { 
            ArticleId = articleId, 
            Name = "Article",
            ItemNumber = "123.456.789",
            StorageBin = "L1",
            ImageUrl = "https://image.url",
            ShoppingUrl = "https://shopping.url",
            Stock = 30,
            MinStock = 10,
            GroupId = groupId,
            OperationAreaId = operationAreaId,
            ProcurementId = procurementId,
            StoragePlaceId = storagePlaceId,
        };

        _articleRepositoryMock.Setup(x => x.GetByIdAsync(articleId)).ReturnsAsync(article);
        _groupRepositoryMock.Setup(x => x.GetByIdAsync(groupId)).ReturnsAsync(group);
        _operationAreaRepositoryMock.Setup(x => x.GetByIdAsync(operationAreaId)).ReturnsAsync(operationArea);
        _procurementRepositoryMock.Setup(x => x.GetByIdAsync(procurementId)).ReturnsAsync(procurement);
        _storagePlaceRepositoryMock.Setup(x => x.GetByIdAsync(storagePlaceId)).ReturnsAsync(storagePlace);

        // Act
        var result = await _sutGetArticle.Handle(getArticleQuery, new CancellationToken());

        // Assert
        _articleRepositoryMock.Verify(x => x.GetByIdAsync(articleId), Times.Once);
        _groupRepositoryMock.Verify(x => x.GetByIdAsync(groupId), Times.Once);
        _operationAreaRepositoryMock.Verify(x => x.GetByIdAsync(operationAreaId), Times.Once);
        _procurementRepositoryMock.Verify(x => x.GetByIdAsync(procurementId), Times.Once);
        _storagePlaceRepositoryMock.Verify(x => x.GetByIdAsync(storagePlaceId), Times.Once);

        Assert.NotNull(result);
        Assert.IsType<GetArticleVm>(result);
        Assert.Equal(article.ArticleId, result.ArticleId);
        Assert.Equal(article.Name, result.Name);
        Assert.Equal(article.ItemNumber, result.ItemNumber);
        Assert.Equal(article.StorageBin, result.StorageBin);
        Assert.Equal(article.ImageUrl, result.ImageUrl);
        Assert.Equal(article.ShoppingUrl, result.ShoppingUrl);
        Assert.Equal(article.Stock, result.Stock);
        Assert.Equal(article.MinStock, result.MinStock);
        Assert.Equal(article.GroupId, result.Group.GroupId);
        Assert.Equal(article.OperationAreaId, result.OperationArea.OperationAreaId);
        Assert.Equal(article.ProcurementId, result.Procurement.ProcurementId);
        Assert.Equal(article.StoragePlaceId, result.StoragePlace.StoragePlaceId);
    }

    [Fact]
    public async Task GetArticles_ShouldGetListOfArticles()
    {
        // Arrange
        var articleId1 = Guid.NewGuid();
        var groupId1 = Guid.NewGuid();
        var operationAreaId1 = Guid.NewGuid();
        var procurementId1 = Guid.NewGuid();
        var storagePlaceId1 = Guid.NewGuid();

        var articleId2 = Guid.NewGuid();
        var groupId2 = Guid.NewGuid();
        var operationAreaId2 = Guid.NewGuid();
        var procurementId2 = Guid.NewGuid();
        var storagePlaceId2 = Guid.NewGuid();

        var groups = new List<Group>
        {
            new Group { GroupId = groupId1, Name = "Group1" },
            new Group { GroupId = groupId2, Name = "Group2" },
        };

        var operationAreas = new List<OperationArea>
        {
            new OperationArea { OperationAreaId = operationAreaId1, Name = "OperationArea1" },
            new OperationArea { OperationAreaId = operationAreaId2, Name = "OperationArea2" },
        };

        var procurements = new List<Procurement>
        {
            new Procurement
            {
                ProcurementId = procurementId1,
                Name = "Procurement",
                Email = "email@email.com",
                Phone = "+49 174 46731134",
                Link = "www.link.com"
            },
            new Procurement
            {
                ProcurementId = procurementId2,
                Name = "Procurement2",
                Email = "email2@email.com",
                Phone = "+49 171 4564615",
                Link = "www.link2.com"
            },
        };

        var storagePlaces = new List<StoragePlace>
        {
            new StoragePlace { StoragePlaceId = storagePlaceId1, Name = "StoragePlace1" },
            new StoragePlace { StoragePlaceId = storagePlaceId2, Name = "StoragePlace2" },
        };


        var articles = new List<Article>
        {
            new Article
            {
                ArticleId = articleId1,
                Name = "Article",
                ItemNumber = "123.456.789",
                StorageBin = "L1",
                ImageUrl = "https://image.url",
                ShoppingUrl = "https://shopping.url",
                Stock = 30,
                MinStock = 10,
                GroupId = groupId1,
                Group = groups[0],
                OperationAreaId = operationAreaId1,
                OperationArea = operationAreas[0],
                ProcurementId = procurementId1,
                Procurement = procurements[0],
                StoragePlaceId = storagePlaceId1,
                StoragePlace = storagePlaces[0]
            },
            new Article
            {
                ArticleId = articleId2,
                Name = "Article2",
                ItemNumber = "234.456.789",
                StorageBin = "L2",
                ImageUrl = "https://image2.url",
                ShoppingUrl = "https://shopping2.url",
                Stock = 55,
                MinStock = 20,
                GroupId = groupId2,
                Group = groups[1],
                OperationAreaId = operationAreaId2,
                OperationArea = operationAreas[1],
                ProcurementId = procurementId2,
                Procurement = procurements[1],
                StoragePlaceId = storagePlaceId2,
                StoragePlace = storagePlaces[1]
            },
        };

        _articleRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(articles);
        _groupRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(groups);
        _operationAreaRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(operationAreas);
        _storagePlaceRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(storagePlaces);
        _procurementRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(procurements);

        // Act
        var result = await _sutGetArticles.Handle(new GetArticlesQuery(), new CancellationToken());

        // Assert
        _articleRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        _groupRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        _operationAreaRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        _procurementRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        _storagePlaceRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);

        Assert.NotNull(result);
        Assert.IsType<List<GetArticlesVm>>(result);
        Assert.Equal(articles.Count, result.Count);
        Assert.Equal(articles[0].Name, result[0].Name);
        Assert.Equal(articles[0].ItemNumber, result[0].ItemNumber);
        Assert.Equal(articles[0].StorageBin, result[0].StorageBin);
        Assert.Equal(articles[0].ImageUrl, result[0].ImageUrl);
        Assert.Equal(articles[0].ShoppingUrl, result[0].ShoppingUrl);
        Assert.Equal(articles[0].Stock, result[0].Stock);
        Assert.Equal(articles[0].MinStock, result[0].MinStock);
        Assert.Equal(articles[0].GroupId, result[0].Group.GroupId);
        Assert.Equal(articles[0].OperationAreaId, result[0].OperationArea.OperationAreaId);
        Assert.Equal(articles[0].ProcurementId, result[0].Procurement.ProcurementId);
        Assert.Equal(articles[0].StoragePlaceId, result[0].StoragePlace.StoragePlaceId);
        Assert.Equal(articles[1].Name, result[1].Name);
        Assert.Equal(articles[1].ItemNumber, result[1].ItemNumber);
        Assert.Equal(articles[1].StorageBin, result[1].StorageBin);
        Assert.Equal(articles[1].ImageUrl, result[1].ImageUrl);
        Assert.Equal(articles[1].ShoppingUrl, result[1].ShoppingUrl);
        Assert.Equal(articles[1].Stock, result[1].Stock);
        Assert.Equal(articles[1].MinStock, result[1].MinStock);
        Assert.Equal(articles[1].GroupId, result[1].Group.GroupId);
        Assert.Equal(articles[1].OperationAreaId, result[1].OperationArea.OperationAreaId);
        Assert.Equal(articles[1].ProcurementId, result[1].Procurement.ProcurementId);
        Assert.Equal(articles[1].StoragePlaceId, result[1].StoragePlace.StoragePlaceId);
    }

    [Fact]
    public async Task AddAsync_ShouldCreateArticle()
    {
        // Arrange
        var createArticleCommand = new CreateArticleCommand 
        {
            Name = "Article",
            ItemNumber = "123.456.789",
            StorageBin = "L1",
            ImageUrl = "https://image.url",
            ShoppingUrl = "https://shopping.url",
            Stock = 30,
            MinStock = 10,
            GroupId = Guid.NewGuid(),
            OperationAreaId = Guid.NewGuid(),
            ProcurementId = Guid.NewGuid(),
            StoragePlaceId = Guid.NewGuid(),
        };

        var returnValue = new Article 
        { 
            ArticleId = Guid.NewGuid(),
            Name = createArticleCommand.Name,
            ItemNumber = createArticleCommand.ItemNumber,
            StorageBin = createArticleCommand.StorageBin,
            ImageUrl = createArticleCommand.ImageUrl,
            ShoppingUrl = createArticleCommand.ShoppingUrl,
            Stock = createArticleCommand.Stock,
            MinStock = createArticleCommand.MinStock,
            GroupId = createArticleCommand.GroupId,
            OperationAreaId = createArticleCommand.OperationAreaId,
            ProcurementId = createArticleCommand.ProcurementId,
            StoragePlaceId = createArticleCommand.StoragePlaceId,
        };

        _articleRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Article>())).ReturnsAsync(returnValue);

        // Act
        var result = await _sutCreateArticle.Handle(createArticleCommand, new CancellationToken());

        // Assert
        _articleRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Article>()), Times.Once);

        Assert.Equal(returnValue.ArticleId, result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteArticle()
    {
        // Arrange
        var articleId = Guid.NewGuid();
        var deletArticleCommand = new DeleteArticleCommand { ArticleId = articleId };
        var article = new Article 
        { 
            ArticleId = articleId,
            Name = "Article",
            ItemNumber = "123.456.789",
            StorageBin = "L1",
            ImageUrl = "https://image.url",
            ShoppingUrl = "https://shopping.url",
            Stock = 30,
            MinStock = 10,
            GroupId = Guid.NewGuid(),
            OperationAreaId = Guid.NewGuid(),
            ProcurementId = Guid.NewGuid(),
            StoragePlaceId = Guid.NewGuid(),
        };

        _articleRepositoryMock.Setup(x => x.GetByIdAsync(articleId)).ReturnsAsync(article);

        // Act
        await _sutDeleteArticle.Handle(deletArticleCommand, new CancellationToken());

        // Assert
        _articleRepositoryMock.Verify(x => x.GetByIdAsync(articleId), Times.Once);
        _articleRepositoryMock.Verify(x => x.DeleteAsync(article), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateArticle()
    {
        // Arrange
        var articleId = Guid.NewGuid();
        var groupId = Guid.NewGuid();
        var operationAreaId = Guid.NewGuid();
        var procurementId = Guid.NewGuid();
        var storagePlaceId = Guid.NewGuid();

        var updateArticleCommand = new UpdateArticleCommand
        {
            ArticleId = articleId,
            Name = "Article",
            ItemNumber = "123.456.789",
            StorageBin = "L1",
            ImageUrl = "https://image.url",
            ShoppingUrl = "https://shopping.url",
            Stock = 30,
            MinStock = 10,
            GroupId = groupId,
            OperationAreaId = operationAreaId,
            ProcurementId = procurementId,
            StoragePlaceId = storagePlaceId,
        };

        var article = new Article
        {
            ArticleId = articleId,
            Name = updateArticleCommand.Name,
            ItemNumber = updateArticleCommand.ItemNumber,
            StorageBin = updateArticleCommand.StorageBin,
            ImageUrl = updateArticleCommand.ImageUrl,
            ShoppingUrl = updateArticleCommand.ShoppingUrl,
            Stock = updateArticleCommand.Stock,
            MinStock = updateArticleCommand.MinStock,
            GroupId = groupId,
            Group = null,
            OperationAreaId = operationAreaId,
            OperationArea = null,
            ProcurementId = procurementId,
            Procurement = null,
            StoragePlaceId = storagePlaceId,
            StoragePlace = null
        };

        _articleRepositoryMock.Setup(x => x.GetByIdAsync(articleId)).ReturnsAsync(article);

        // Act
        await _sutUpdateArticle.Handle(updateArticleCommand, new CancellationToken());

        // Assert
        _articleRepositoryMock.Verify(x => x.GetByIdAsync(articleId), Times.Once);
        _articleRepositoryMock.Verify(x => x.UpdateAsync(article), Times.Once);
        Assert.Equal(updateArticleCommand.ArticleId, article.ArticleId);
        Assert.Equal(updateArticleCommand.Name, article.Name);
        Assert.Equal(updateArticleCommand.ItemNumber, article.ItemNumber);
        Assert.Equal(updateArticleCommand.StorageBin, article.StorageBin);
        Assert.Equal(updateArticleCommand.ImageUrl, article.ImageUrl);
        Assert.Equal(updateArticleCommand.ShoppingUrl, article.ShoppingUrl);
        Assert.Equal(updateArticleCommand.Stock, article.Stock);
        Assert.Equal(updateArticleCommand.MinStock, article.MinStock);
        Assert.Equal(updateArticleCommand.GroupId, article.GroupId);
        Assert.Equal(updateArticleCommand.OperationAreaId, article.OperationAreaId);
        Assert.Equal(updateArticleCommand.ProcurementId, article.ProcurementId);
        Assert.Equal(updateArticleCommand.StoragePlaceId, article.StoragePlaceId);
        Assert.NotEqual(default(DateTime), article.LastModifiedDate);
    }
}