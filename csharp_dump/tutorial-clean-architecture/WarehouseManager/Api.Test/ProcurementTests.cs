using Application.Contracts.Persistence;
using Application.Features.Procurements.Commands.Create;
using Application.Features.Procurements.Commands.Delete;
using Application.Features.Procurements.Commands.Update;
using Application.Features.Procurements.Queries.GetProcurement;
using Application.Features.Procurements.Queries.GetProcurements;
using Domain.Entities;
using Moq;

namespace Api.Test;

public class ProcurementTests
{
    private readonly GetProcurementQueryHandler _sutGetProcurement;
    private readonly GetProcurementsQueryHandler _sutGetProcurements;
    private readonly CreateProcurementCommandHandler _sutCreateProcurement;
    private readonly DeleteProcurementCommandHandler _sutDeleteProcurement;
    private readonly UpdateProcurementCommandHandler _sutUpdateProcurement;
    private readonly Mock<IProcurementRepository> _procurementRepositoryMock = new();
    private readonly Mock<IArticleRepository> _articleRepositoryMock = new();


    public ProcurementTests()
    {
        _sutGetProcurement = new GetProcurementQueryHandler(_procurementRepositoryMock.Object);
        _sutGetProcurements = new GetProcurementsQueryHandler(_procurementRepositoryMock.Object);
        _sutCreateProcurement = new CreateProcurementCommandHandler(_procurementRepositoryMock.Object);
        _sutDeleteProcurement = new DeleteProcurementCommandHandler(_procurementRepositoryMock.Object, _articleRepositoryMock.Object);
        _sutUpdateProcurement = new UpdateProcurementCommandHandler(_procurementRepositoryMock.Object, _articleRepositoryMock.Object);
    }

    [Fact]
    public async Task GetProcurement_ShouldGetProcurement()
    {
        // Arrange
        var procurementId = Guid.NewGuid();
        var getProcurementQuery = new GetProcurementQuery { ProcurementId = procurementId };
        var procurement = new Procurement
        {
            ProcurementId = procurementId,
            Name = "Procurement",
            Email = "email@email.com",
            Phone = "+49 174 46731134",
            Link = "www.link.com"
        };

        _procurementRepositoryMock.Setup(x => x.GetByIdAsync(procurementId)).ReturnsAsync(procurement);

        // Act
        var result = await _sutGetProcurement.Handle(getProcurementQuery, new CancellationToken());

        // Assert
        _procurementRepositoryMock.Verify(x => x.GetByIdAsync(procurementId), Times.Once);

        Assert.NotNull(result);
        Assert.IsType<GetProcurementVm>(result);
        Assert.Equal(procurement.ProcurementId, result.ProcurementId);
        Assert.Equal(procurement.Name, result.Name);
        Assert.Equal(procurement.Email, result.Email);
        Assert.Equal(procurement.Phone, result.Phone);
        Assert.Equal(procurement.Link, result.Link);
    }

    [Fact]
    public async Task GetProcurements_ShouldGetListOfProcurements()
    {
        // Arrange
        var procurements = new List<Procurement>
        {
            new Procurement
            {
                ProcurementId = Guid.NewGuid(),
                Name = "Procurement",
                Email = "email@email.com",
                Phone = "+49 174 46731134",
                Link = "www.link.com"
            },
            new Procurement
            {
                ProcurementId = Guid.NewGuid(),
                Name = "Procurement2",
                Email = "email2@email.com",
                Phone = "+49 171 4564615",
                Link = "www.link2.com"
            },
        };
        _procurementRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(procurements);

        // Act
        var result = await _sutGetProcurements.Handle(new GetProcurementsQuery(), new CancellationToken());

        // Assert
        _procurementRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);

        Assert.NotNull(result);
        Assert.IsType<List<GetProcurementsVm>>(result);
        Assert.Equal(procurements.Count, result.Count);
        Assert.Equal(procurements[0].Name, result[0].Name);
        Assert.Equal(procurements[1].Name, result[1].Name);
    }

    [Fact]
    public async Task AddAsync_ShouldCreateProcurement()
    {
        // Arrange
        var createProcurementCommand = new CreateProcurementCommand
        {
            Name = "Procurement",
            Email = "email@email.com",
            Phone = "+49174123456",
            Link = "www.link.com"
        };

        var returnValue = new Procurement 
        { 
            ProcurementId = Guid.NewGuid(),
            Name = createProcurementCommand.Name,
            Email = createProcurementCommand.Email,
            Phone = createProcurementCommand.Phone,
            Link = createProcurementCommand.Link
        };

        _procurementRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Procurement>())).ReturnsAsync(returnValue);

        // Act
        var result = await _sutCreateProcurement.Handle(createProcurementCommand, new CancellationToken());

        // Assert
        _procurementRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Procurement>()), Times.Once);

        Assert.Equal(returnValue.ProcurementId, result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteProcurement()
    {
        // Arrange
        var procurementId = Guid.NewGuid();
        var deleteProcurementCommand = new DeleteProcurementCommand { ProcurementId = procurementId };
        var procurement = new Procurement
        {
            ProcurementId = procurementId,
            Name = "Procurement",
            Email = "email@email.com",
            Phone = "+49 174 46731134",
            Link = "www.link.com"
        };

        _procurementRepositoryMock.Setup(x => x.GetByIdAsync(procurementId)).ReturnsAsync(procurement);
        _articleRepositoryMock.Setup(x => x.FindAnyArticleWithEntityId(It.IsAny<Func<Article, bool>>())).ReturnsAsync(false);

        // Act
        await _sutDeleteProcurement.Handle(deleteProcurementCommand, new CancellationToken());

        // Assert
        _procurementRepositoryMock.Verify(x => x.GetByIdAsync(procurementId), Times.Once);
        _articleRepositoryMock.Verify(x => x.FindAnyArticleWithEntityId(It.IsAny<Func<Article, bool>>()), Times.Once);
        _procurementRepositoryMock.Verify(x => x.DeleteAsync(procurement), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProcurement()
    {
        // Arrange
        var procurementId = Guid.NewGuid();
        var updateProcurementCommand = new UpdateProcurementCommand
        {
            ProcurementId = procurementId,
            Name = "Procurement",
            Email = "email@email.com",
            Phone = "+49174123456",
            Link = "www.link.com"
        };

        var procurement = new Procurement
        {
            ProcurementId = procurementId,
            Name = updateProcurementCommand.Name,
            Email = updateProcurementCommand.Email,
            Phone = updateProcurementCommand.Phone, 
            Link = updateProcurementCommand.Link
        };

        _procurementRepositoryMock.Setup(x => x.GetByIdAsync(procurementId)).ReturnsAsync(procurement);

        // Act
        await _sutUpdateProcurement.Handle(updateProcurementCommand, new CancellationToken());

        // Assert
        _procurementRepositoryMock.Verify(x => x.GetByIdAsync(procurementId), Times.Once);
        _procurementRepositoryMock.Verify(x => x.UpdateAsync(procurement), Times.Once);
        Assert.Equal(updateProcurementCommand.ProcurementId, procurement.ProcurementId);
        Assert.Equal(updateProcurementCommand.Name, procurement.Name);
        Assert.Equal(updateProcurementCommand.Email, procurement.Email);
        Assert.Equal(updateProcurementCommand.Phone, procurement.Phone);
        Assert.Equal(updateProcurementCommand.Link, procurement.Link);
        Assert.NotEqual(default(DateTime), procurement.LastModifiedDate);
    }
}