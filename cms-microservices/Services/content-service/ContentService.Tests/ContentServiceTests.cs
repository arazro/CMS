using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using ContentService.Application.Services;
using ContentService.Domain.Entities;
using ContentService.Domain.Enums;
using ContentService.Domain.Interfaces;
using ContentService.Application.DTOs;
using ContentService.Application.Interfaces;
using static System.Net.Mime.MediaTypeNames;
using ContentService.Infrastructure.Services;
using ContentService.Tests.Helpers;
using System.Net.Http;

public class ContentServiceTests
{
    private readonly Mock<IContentRepository> _contentRepoMock;
    private readonly Mock<IUserServiceClient> _userClientMock;
    private readonly ContentService.Application.Services.ContentService _contentService;
   // private UserServiceClient _client;

    public ContentServiceTests()
    {
        _contentRepoMock = new Mock<IContentRepository>();
        _userClientMock = new Mock<IUserServiceClient>();
        _contentService = new ContentService.Application.Services.ContentService(_contentRepoMock.Object, _userClientMock.Object);
        var configuration = TestConfigurationBuilder.BuildConfiguration();
        //var baseUrl = configuration["UserService:BaseUrl"];

        //_client = new UserServiceClient(new HttpClient
        //{
        //    BaseAddress = new Uri(baseUrl)
        //});
    }

    [Fact]
    public async Task GetAllContentsAsync_ShouldReturnAllContents()
    {
        var contents = new List<Content> { new Content ( "Title", "Body",  "Author" ) };
        _contentRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(contents);

        var result = await _contentService.GetAllContentsAsync();

        Assert.NotNull(result);
        Assert.Single(result);
    }



    [Fact]
    public async Task GetContentByIdAsync_InvalidId_ShouldReturnNull()
    {
        _contentRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Content?)null);

        var result = await _contentService.GetContentByIdAsync(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateContentAsync_ValidAuthor_ShouldCreateContent()
    {
        var content = new Content { AuthorId = Guid.NewGuid(), Title = "New Content" };
        _userClientMock.Setup(c => c.GetUserByIdAsync(content.AuthorId)).ReturnsAsync(new UserDto { Id = content.AuthorId, Name = "Author" });

        var result = await _contentService.CreateContentAsync(content);

        Assert.Equal(ContentStatus.Archived, result.Status);
        Assert.Equal("Author", result.Author);
    }

    [Fact]
    public async Task CreateContentAsync_InvalidAuthor_ShouldThrowException()
    {
        var content = new Content { AuthorId = Guid.NewGuid() };
        _userClientMock.Setup(c => c.GetUserByIdAsync(content.AuthorId)).ReturnsAsync((UserDto?)null);

        await Assert.ThrowsAsync<Exception>(() => _contentService.CreateContentAsync(content));
    }

    [Fact]
    public async Task UpdateContentAsync_ExistingContent_ShouldUpdateAndReturnTrue()
    {
        var id = Guid.NewGuid();
        var existing = new Content {  Title = "Old" };
        var updated = new Content { Title = "Updated", Body = "Body", Category = "Cat" };

        _contentRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(existing);

        var result = await _contentService.UpdateContentAsync(id, updated);

        Assert.True(result);
        _contentRepoMock.Verify(r => r.UpdateAsync(It.IsAny<Content>()), Times.Once);
    }

    [Fact]
    public async Task UpdateContentAsync_NonExistingContent_ShouldReturnFalse()
    {
        _contentRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Content?)null);

        var result = await _contentService.UpdateContentAsync(Guid.NewGuid(), new Content());

        Assert.False(result);
    }

    [Fact]
    public async Task DeleteContentAsync_ExistingContent_ShouldDeleteAndReturnTrue()
    {
        var id = Guid.NewGuid();
        var content = new Content ();
        _contentRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(content);

        var result = await _contentService.DeleteContentAsync(id);

        Assert.True(result);
        _contentRepoMock.Verify(r => r.DeleteAsync(id), Times.Once);
    }

    [Fact]
    public async Task DeleteContentAsync_NonExistingContent_ShouldReturnFalse()
    {
        _contentRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Content?)null);

        var result = await _contentService.DeleteContentAsync(Guid.NewGuid());

        Assert.False(result);
    }

    [Fact]
    public async Task UpdateAuthorInfoAsync_Success_ShouldReturnTrue()
    {
        var authorId = Guid.NewGuid();
        var dto = new UpdateUserDto { Name = "New Name" };

        _userClientMock.Setup(c => c.UpdateUserAsync(authorId, dto)).ReturnsAsync(true);

        var result = await _contentService.UpdateAuthorInfoAsync(authorId, dto);

        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAuthorInfoAsync_Failure_ShouldThrowException()
    {
        var authorId = Guid.NewGuid();
        var dto = new UpdateUserDto { Name = "New Name" };

        _userClientMock.Setup(c => c.UpdateUserAsync(authorId, dto)).ReturnsAsync(false);

        await Assert.ThrowsAsync<Exception>(() => _contentService.UpdateAuthorInfoAsync(authorId, dto));
    }
}
