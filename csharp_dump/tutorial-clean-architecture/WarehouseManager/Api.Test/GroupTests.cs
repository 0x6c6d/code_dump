using Application.Contracts.Persistence;
using Application.Features.Groups.Commands.Create;
using Application.Features.Groups.Commands.Delete;
using Application.Features.Groups.Commands.Update;
using Application.Features.Groups.Queries.GetGroup;
using Application.Features.Groups.Queries.GetGroups;
using Domain.Entities;
using Moq;

namespace Api.Test;

public class GroupTests
{
    private readonly GetGroupQueryHandler _sutGetGroup;
    private readonly GetGroupsQueryHandler _sutGetGroups;
    private readonly CreateGroupCommandHandler _sutCreateGroup;
    private readonly DeleteGroupCommandHandler _sutDeleteGroup;
    private readonly UpdateGroupCommandHandler _sutUpdateGroup;
    private readonly Mock<IGroupRepository> _groupRepositoryMock = new();
    private readonly Mock<IArticleRepository> _articleRepositoryMock = new();
    

    public GroupTests()
    {
        _sutGetGroup = new GetGroupQueryHandler(_groupRepositoryMock.Object);
        _sutGetGroups = new GetGroupsQueryHandler(_groupRepositoryMock.Object);
        _sutCreateGroup = new CreateGroupCommandHandler(_groupRepositoryMock.Object);
        _sutDeleteGroup = new DeleteGroupCommandHandler(_groupRepositoryMock.Object, _articleRepositoryMock.Object);
        _sutUpdateGroup = new UpdateGroupCommandHandler(_groupRepositoryMock.Object);
    }

    [Fact]
    public async Task GetGroup_ShouldGetGroup()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var getGroupQuery = new GetGroupQuery { GroupId = groupId };
        var group = new Group { GroupId = groupId, Name = "Group" };
        _groupRepositoryMock.Setup(x => x.GetByIdAsync(groupId)).ReturnsAsync(group);

        // Act
        var result = await _sutGetGroup.Handle(getGroupQuery, new CancellationToken());

        // Assert
        _groupRepositoryMock.Verify(x => x.GetByIdAsync(groupId), Times.Once);

        Assert.NotNull(result);
        Assert.IsType<GetGroupVm>(result);
        Assert.Equal(group.GroupId, result.GroupId);
        Assert.Equal(group.Name, result.Name);
    }

    [Fact]
    public async Task GetGroups_ShouldGetListOfGroups()
    {
        // Arrange
        var groups = new List<Group>
        {
            new Group { GroupId = Guid.NewGuid(), Name = "Group1" },
            new Group { GroupId = Guid.NewGuid(), Name = "Group2" },
        };
        _groupRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(groups);

        // Act
        var result = await _sutGetGroups.Handle(new GetGroupsQuery(), new CancellationToken());

        // Assert
        _groupRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);

        Assert.NotNull(result);
        Assert.IsType<List<GetGroupsVm>>(result);
        Assert.Equal(groups.Count, result.Count);
        Assert.Equal(groups[0].Name, result[0].Name);
        Assert.Equal(groups[1].Name, result[1].Name);
    }

    [Fact]
    public async Task AddAsync_ShouldCreateGroup()
    {
        // Arrange
        var createGroupCommand = new CreateGroupCommand { Name = "Group" };
        var returnValue = new Group { GroupId = Guid.NewGuid(), Name = createGroupCommand.Name };
        _groupRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Group>())).ReturnsAsync(returnValue);

        // Act
        var result = await _sutCreateGroup.Handle(createGroupCommand, new CancellationToken());

        // Assert
        _groupRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Group>()), Times.Once);

        Assert.Equal(returnValue.GroupId, result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteGroup()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var deleteGroupCommand = new DeleteGroupCommand { GroupId = groupId };
        var group = new Group { GroupId = groupId, Name = "Group" };
        _groupRepositoryMock.Setup(x => x.GetByIdAsync(groupId)).ReturnsAsync(group);
        _articleRepositoryMock.Setup(x => x.FindAnyArticleWithEntityId(It.IsAny<Func<Article, bool>>())).ReturnsAsync(false);

        // Act
        await _sutDeleteGroup.Handle(deleteGroupCommand, new CancellationToken());

        // Assert
        _groupRepositoryMock.Verify(x => x.GetByIdAsync(groupId), Times.Once);
        _articleRepositoryMock.Verify(x => x.FindAnyArticleWithEntityId(It.IsAny<Func<Article, bool>>()), Times.Once);
        _groupRepositoryMock.Verify(x => x.DeleteAsync(group), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateGroup()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var groupName = "Group";
        var updateGroupCommand = new UpdateGroupCommand
        {
            GroupId = groupId,
            Name = groupName
        };

        var group = new Group
        {
            GroupId = groupId,
            Name = groupName
        };

        _groupRepositoryMock.Setup(x => x.GetByIdAsync(groupId)).ReturnsAsync(group);

        // Act
        await _sutUpdateGroup.Handle(updateGroupCommand, new CancellationToken());

        // Assert
        _groupRepositoryMock.Verify(x => x.GetByIdAsync(groupId), Times.Once);
        _groupRepositoryMock.Verify(x => x.UpdateAsync(group), Times.Once);
        Assert.Equal(updateGroupCommand.GroupId, group.GroupId);
        Assert.Equal(updateGroupCommand.Name, group.Name);
        Assert.NotEqual(default(DateTime), group.LastModifiedDate);
    }
}